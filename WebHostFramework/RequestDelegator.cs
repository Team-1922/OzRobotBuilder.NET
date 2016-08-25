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
    public class RequestDelegator : RequestDelegatorWebHooks, IDisposable
    {
        public RequestDelegator(IHierarchialAccessRoot data, string pathRoot) : base(pathRoot)
        {
            _data = data;
        }

        public IHierarchialAccessRoot Data
        {
            get
            {
                if (null == _data)
                    throw new NullReferenceException("RequestDelegator.Data Must Be Initialized Before Using!");
                return _data;
            }
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
        protected override async Task<BasicHttpResponse> AggregateRequestAsync(string method, string path, string body)
        {
            //convert the path to '.' delimeters and remove the leading and trailing '.'
            path = ConvertPath(path).Trim('.');
            switch (method)
            {
                case "GET":
                    return await GetAsync(path);
                case "POST":
                    return await PostAsync(path, body);
                case "PUT":
                    return await PutAsync(path, body);
                case "PATCH":
                    return await PatchAsync(path, body);
                case "DELETE":
                    return await DeleteAsync(path);
                default:
                    return new BasicHttpResponse() { StatusCode = 405 };//not allowed
            }
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
