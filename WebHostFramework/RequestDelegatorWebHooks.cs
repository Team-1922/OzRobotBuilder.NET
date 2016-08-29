using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework
{
    /// <summary>
    /// a request delegator which has web hooks enabled on it
    /// </summary>
    public abstract class RequestDelegatorWebHooks : IRequestDelegator
    {
        public RequestDelegatorWebHooks(string pathRoot)
        {
            //if (pathRoot == "") ;//TODO: do something
            PathRoot = pathRoot;
        }
        
        public async Task SendWebHooksCallbackRequest(string method, string path, string body)
        {
            string convertedPath = ConvertPathReverse(path);
            foreach(var subscriber in _webHooksSubscribers)
            {
                WebRequest request = WebRequest.Create($"{subscriber.ToString()}{convertedPath}");
                request.Method = method;
                using (var sendStream = await request.GetRequestStreamAsync())
                {
                    var sendStreamWriter = new StreamWriter(sendStream);
                    await sendStreamWriter.WriteAsync(body);
                }
            }
        }

        #region CRUD WebHooks Methods
        private async Task<BasicHttpResponse> HooksGetAsync()
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                response.Body = await _webHooksSubscribers.GetJsonAsync();

                //OK
                response.StatusCode = 200;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> HookPostAsync(Uri uri)
        {
            FailIfNull(uri);
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                if (_webHooksSubscribers.Add(uri))
                {
                    //created
                    response.StatusCode = 201;
                }
                else
                {
                    //conflict
                    response.StatusCode = 409;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> HookPutAsync(Uri uri)
        {
            FailIfNull(uri);
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                if (_webHooksSubscribers.Add(uri))
                {
                    //created
                    response.StatusCode = 201;
                }
                else
                {
                    //OK
                    response.StatusCode = 200;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> HookDeleteAsync(Uri uri)
        {
            FailIfNull(uri);
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                if (_webHooksSubscribers.Remove(uri))
                {
                    //OK
                    response.StatusCode = 200;
                }
                else
                {
                    //not found
                    response.StatusCode = 404;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        #endregion

        #region IRequestDelegator
        public string PathRoot { get; }
        public async Task<BasicHttpResponse> ProcessRequestAsync(string method, string path, string body)
        {
            //split the path after PathRoot
            string fullRequestPath = path;
            string requestPath = fullRequestPath;
            if (fullRequestPath != _webHooksPath)
            {
                requestPath = GetPath(fullRequestPath);
                if (null == requestPath)
                {
                    //this means the path was invalid
                    return new BasicHttpResponse() { StatusCode = 404 };
                }
            }

            //call the correct method for this request
            if (requestPath == _webHooksPath)
            {
                Uri subscriberUri = null;
                if (method != "GET")
                {
                    try
                    {
                        subscriberUri = new Uri(body);
                    }
                    catch (Exception)
                    {
                        return new BasicHttpResponse() { StatusCode = 400 };
                    }
                }
                try
                {
                    switch (method)
                    {
                        case "GET":
                            return await HooksGetAsync();
                        case "POST":
                            return await HookPostAsync(subscriberUri);
                        case "PUT":
                            return await HookPutAsync(subscriberUri);
                        case "DELETE":
                            return await HookDeleteAsync(subscriberUri);
                        default:
                            return new BasicHttpResponse() { StatusCode = 405 };//not allowed
                    }
                }
                catch (Exception)
                {
                    return new BasicHttpResponse() { StatusCode = 400 };
                }
            }
            else
            {
                return await AggregateRequestAsync(method, requestPath, body);
            }
        }
        #endregion

        #region Abstract Methods
        protected abstract Task<BasicHttpResponse> AggregateRequestAsync(string method, string path, string body);
        #endregion

        #region Helper Methods
        protected string GetPath(string fullPath)
        {
            var split = fullPath.Split(new string[] { PathRoot }, StringSplitOptions.None);
            if (split.Length != 2)
                return null;
            return split[1];
        }
        protected static string ConvertPath(string key)
        {
            return key.Replace('/', '.');
        }
        protected static string ConvertPathReverse(string key)
        {
            return key.Replace('.', '/');
        }
        protected static void FailIfNull(object obj)
        {
            if (obj == null)
                throw new Exception();
        }
        #endregion

        #region Private Fields
        private const string _webHooksPath = "/_hooks_";
        private WebHooksSubscriberCollection _webHooksSubscribers = new WebHooksSubscriberCollection();
        #endregion
    }
}
