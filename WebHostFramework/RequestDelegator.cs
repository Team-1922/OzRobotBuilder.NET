using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models.XML;

namespace Team1922.WebFramework
{
    public class RequestDelegator : IDisposable
    {
        public RequestDelegator(IHierarchialAccessRoot data)
        {
            Data = data;
        }

        public IHierarchialAccessRoot Data
        {
            get
            {
                if (null == _data)
                    throw new NullReferenceException("RequestDelegator.Data Must Be Initialized Before Using!");
                return _data;
            }

            set
            {
                _data = value;
            }
        }
        public async Task ProcessRequestAsync(HttpContext context)
        {
            //split the path after "api/Robot"
            string fullRequestPath = context.Request.Path.Value;
            string requestPath = GetPath(fullRequestPath);
            if(null == requestPath)
            {
                //this means the path was invalid
                context.Response.StatusCode = 404;
                return;
            }
            requestPath = ConvertPath(requestPath);

            //get the request body
            var bufferReader = new StreamReader(context.Request.Body);            
            string requestBody = await bufferReader.ReadToEndAsync();

            //call the correct method for this request
            BasicHttpResponse response = await AggregateMethodAsync(context.Request.Method, requestPath, requestBody);

            //set the status code of the response
            context.Response.StatusCode = response.StatusCode;   

            //this needs to go last!
            await context.Response.WriteAsync(response.Body ?? "");
        }

        #region CRUD Access Methods
        private async Task<BasicHttpResponse> GetAsync(string path)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                response.Body = await Data.GetAsync(path);

                //OK
                response.StatusCode = 200;
            }
            catch (ArgumentException)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> PostAsync(string path, string body)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                //only do this operation if it does not exists already
                if (Data.KeyExists(path))
                    response.StatusCode = 409;
                else
                {
                    await Data.SetAsync(path, body);

                    //created
                    response.StatusCode = 201;
                }
            }
            catch (ArgumentException e)
            {
                //not found
                response.StatusCode = 404;
            }
            catch(FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = 400;
                response.Body = e.Message;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> PutAsync(string path, string body)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                //set whether it exists or not
                await Data.SetAsync(path, body);

                //created
                response.StatusCode = 201;
            }
            catch (ArgumentException e)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = 400;
                response.Body = e.Message;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> PatchAsync(string path, string body)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                if (!Data.KeyExists(path))
                    throw new ArgumentException();

                //only do this operation if it exists already
                await Data.SetAsync(path, body);

                //OK
                response.StatusCode = 200;
            }
            catch (ArgumentException)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = 400;
                response.Body = e.Message;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        private async Task<BasicHttpResponse> DeleteAsync(string path)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                //null indicates we want this object to be deleted
                await Data.SetAsync(path, null);

                //OK
                response.StatusCode = 200;
            }
            catch (ArgumentException)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = 400;
                response.Body = e.Message;
            }
            catch(NullReferenceException)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        #endregion

        #region Private Helper Methods
        private static string GetPath(string fullPath)
        {
            var split = fullPath.Split(new string[] { "api/Robot/" }, StringSplitOptions.None);
            if (split.Length != 2)
                return null;
            return split[1];
        }
        private async Task<BasicHttpResponse> AggregateMethodAsync(string method, string requestPath, string requestBody)
        {
            switch (method)
            {
                case "GET":
                    return await GetAsync(requestPath);
                case "POST":
                    return await PostAsync(requestPath, requestBody);
                case "PUT":
                    return await PutAsync(requestPath, requestBody);
                case "PATCH":
                    return await PatchAsync(requestPath, requestBody);
                case "DELETE":
                    return await DeleteAsync(requestPath);
                default:
                    return new BasicHttpResponse() { StatusCode = 503 };//Is this the right status code?
            }
        }
        private static string ConvertPath(string key)
        {
            return key.Replace('/', '.');
        }
        #endregion

        #region Private Data Types
        private struct BasicHttpResponse
        {
            public string Body { get; set; }
            public int StatusCode { get; set; }
        }
        #endregion

        #region Private Fields
        private IHierarchialAccessRoot _data;
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
                    Data.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ViewModelBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
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
