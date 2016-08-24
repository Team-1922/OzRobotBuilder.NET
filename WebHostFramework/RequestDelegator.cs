using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models.XML;

namespace Team1922.WebFramework
{
    public class RequestDelegator
    {
        public static string ConvertPath(string key)
        {
            return key.Replace('/', '.');
        }

        public struct BasicHttpResponse
        {
            public string Body { get; set; }
            public int StatusCode { get; set; }
        }

        #region CRUD Access Methods
        protected async Task<BasicHttpResponse> GetAsync(string path)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                response.Body = await RobotRepository.Instance.GetAsync(ConvertPath(path));

                //OK
                response.StatusCode = 200;
            }
            catch (ArgumentException)
            {
                //not found
                response.StatusCode = 404;
            }
            catch (Exception e)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        protected async Task<BasicHttpResponse> PostAsync(string path, string body)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            var convertedPath = ConvertPath(path);
            try
            {
                //only do this operation if it does not exists already
                if (RobotRepository.Instance.KeyExists(convertedPath))
                    response.StatusCode = 409;
                else
                {
                    await RobotRepository.Instance.SetAsync(convertedPath, body);

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
            catch (Exception e)
            {
                //internal server error
                response.StatusCode = 500;
            }
            return response;
        }
        protected async Task<BasicHttpResponse> PutAsync(string path, string body)
        {
            return new BasicHttpResponse() { StatusCode = 501 };//not implemented
        }
        protected async Task<BasicHttpResponse> PatchAsync(string path, string body)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            var convertedPath = ConvertPath(path);
            try
            {
                // TODO: this IS a slow way to do it
                if (!RobotRepository.Instance.KeyExists(convertedPath))
                    throw new ArgumentException();

                //only do this operation if it exists already
                await RobotRepository.Instance.SetAsync(convertedPath, body);

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
        protected async Task<BasicHttpResponse> DeleteAsync(string path)
        {
            return new BasicHttpResponse() { StatusCode = 501 };//not implemented
        }
        #endregion

        private string GetPath(string fullPath)
        {
            var split = fullPath.Split(new string[] { "api/Robot/" }, StringSplitOptions.None);
            if (split.Length != 2)
                return null;
            return split[1];
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
        public async Task<BasicHttpResponse> AggregateMethodAsync(string method, string requestPath, string requestBody)
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
    }
}
