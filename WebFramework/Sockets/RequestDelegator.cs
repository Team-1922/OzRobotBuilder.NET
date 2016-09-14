using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class RequestDelegator : IRequestDelegator
    {
        public RequestDelegator(IHierarchialAccessRoot data)
        {
            Data = data;
        }

        #region Protocall Methods
        public async Task<Response> GetAsync(string path)
        {
            Response response = new Response();
            try
            {
                response.Body = await Data.GetAsync(path);

                //OK
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (ArgumentException e)
            {
                //not found
                response.StatusCode = HttpStatusCode.NotFound;
                response.Body = e.Message;
            }
            catch (Exception e)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Body = e.Message;
            }
            return response;
        }
        public async Task<Response> SetAsync(string path, string body)
        {
            Response response = new Response();
            try
            {
                await Data.SetAsync(path, body);

                //OK
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (ArgumentException)
            {
                //not found
                response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Body = e.Message;
            }
            catch (Exception)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<Response> AddAsync(string path, string body)
        {
            Response response = new Response();
            try
            {
                bool result = await Data.AddAsync(path, body);

                if (result)
                    response.StatusCode = HttpStatusCode.Created;
                else
                    response.StatusCode = HttpStatusCode.Conflict;
            }
            catch (ArgumentException e)
            {
                //not found
                response.StatusCode = HttpStatusCode.NotFound;
                response.Body = e.Message;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Body = e.Message;
            }
            catch (Exception e)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Body = e.Message;
            }
            return response;
        }
        public async Task<Response> DeleteAsync(string path)
        {
            Response response = new Response();
            try
            {
                bool result = await Data.DeleteAsync(path);

                if (result)
                    response.StatusCode = HttpStatusCode.OK;
                else
                    response.StatusCode = HttpStatusCode.NotFound;
            }
            catch (ArgumentException e)
            {
                //not found
                response.StatusCode = HttpStatusCode.NotFound;
                response.Body = e.Message;
            }
            catch (FacetValidationException e)
            {
                //Bad Request
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Body = e.Message;
            }
            catch (Exception e)
            {
                //internal server error
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Body = e.Message;
            }
            return response;
        }
        #endregion

        #region IRequestDelegator
        public IHierarchialAccessRoot Data { get; }
        public async Task<Response> ProcessRequestAsync(Request request)
        {
            switch (request.Method)
            {
                case Protocall.Method.Get:
                    return await GetAsync(request.Path);
                case Protocall.Method.Set:
                    return await SetAsync(request.Path, request.Body);
                case Protocall.Method.Add:
                    return await AddAsync(request.Path, request.Body);
                case Protocall.Method.Delete:
                    return await DeleteAsync(request.Path);
                default:
                    return new Response() { StatusCode = HttpStatusCode.MethodNotAllowed };//not allowed
            }
        }
        #endregion        
    }
}
