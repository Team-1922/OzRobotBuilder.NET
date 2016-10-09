using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// An implenetation of the request delegator which uses the <see cref="Protocall.Method"/> protocall
    /// </summary>
    public class RequestDelegator : IRequestDelegator
    {
        /// <summary>
        /// creates a request delegator with the given data
        /// </summary>
        /// <param name="data">the data to modify/acces with each request</param>
        public RequestDelegator(IHierarchialAccessRoot data)
        {
            Data = data;
        }

        #region Protocall Methods
        /// <summary>
        /// The <see cref="Protocall.Method.Get"/> Method
        /// </summary>
        /// <param name="path">The path to get data at</param>
        /// <returns>the data retrieved from <paramref name="path"/> </returns>
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
        /// <summary>
        /// The <see cref="Protocall.Method.Set"/> Method
        /// </summary>
        /// <param name="path">The path to set the data to</param>
        /// <param name="body">The data to set</param>
        /// <returns></returns>
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
        /// <summary>
        /// The <see cref="Protocall.Method.Add"/> Method
        /// </summary>
        /// <param name="path">the path to create new data at</param>
        /// <param name="body">the value of this new entry</param>
        /// <returns></returns>
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
        /// <summary>
        /// he <see cref="Protocall.Method.Delete"/>  Method
        /// </summary>
        /// <param name="path">the path of the data to delete</param>
        /// <returns></returns>
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
        /// <summary>
        /// The data which this request delegator acts upon
        /// </summary>
        public IHierarchialAccessRoot Data { get; }
        /// <summary>
        /// Processes a given according to the <see cref="Protocall.Method"/> protocall
        /// </summary>
        /// <param name="request">the request to process</param>
        /// <returns>the result of the processed request</returns>
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
