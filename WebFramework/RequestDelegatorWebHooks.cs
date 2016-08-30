using Microsoft.Net.Http.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

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
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
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
                    response.StatusCode = HttpStatusCode.Created;
                }
                else
                {
                    //conflict
                    response.StatusCode = HttpStatusCode.Conflict;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
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
                    response.StatusCode = HttpStatusCode.Created;
                }
                else
                {
                    //OK
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
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
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    //not found
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
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
            
            //with the new WebListener, this is no longer required
            /*if (fullRequestPath != _webHooksPath)
            {
                requestPath = GetPath(fullRequestPath);
                if (null == requestPath)
                {
                    //this means the path was invalid
                    return new BasicHttpResponse() { StatusCode = HttpStatusCode.NotFound };
                }
            }*/

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
                        return new BasicHttpResponse() { StatusCode = HttpStatusCode.BadRequest };
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
                            return new BasicHttpResponse() { StatusCode = HttpStatusCode.MethodNotAllowed };//not allowed
                    }
                }
                catch (Exception)
                {
                    return new BasicHttpResponse() { StatusCode = HttpStatusCode.BadRequest };
                }
            }
            else
            {
                return await AggregateRequestAsync(method, requestPath, body);
            }
        }
        public async Task StartupServer()
        {
            try
            {
                var listener = new WebListener();
                listener.UrlPrefixes.Add($"http://localhost:8082{PathRoot}");

                listener.Start();

                do
                {
                    using (RequestContext context = await listener.GetContextAsync())
                    {
                        //get the body
                        var requestBodyStream = new StreamReader(context.Request.Body);
                        var requestBody = await requestBodyStream.ReadToEndAsync();

                        context.Response.Headers.Add("content-type", new string[] { "application/json" });
                        var response = await ProcessRequestAsync(context.Request.Method, context.Request.Path, requestBody);

                        context.Response.StatusCode = (int)response.StatusCode;

                        byte[] buffer = Encoding.UTF8.GetBytes(response.Body ?? "");
                        await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                    }
                } while (!_webListenerCTS.IsCancellationRequested);
            }
            catch(Exception e)
            {
                //TODO: do something here
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
        private CancellationTokenSource _webListenerCTS = new CancellationTokenSource();
        private WebListener _webListener;
        private const string _webHooksPath = "/_hooks_";
        private WebHooksSubscriberCollection _webHooksSubscribers = new WebHooksSubscriberCollection();
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _webListenerCTS.Cancel();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RequestDelegatorWebHooks() {
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
