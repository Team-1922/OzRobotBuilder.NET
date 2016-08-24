using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        protected async Task<BasicHttpResponse> Get(string path)
        {
            BasicHttpResponse response = new BasicHttpResponse();
            try
            {
                response.Body = (RobotRepository.Instance[ConvertPath(path)]);
                response.StatusCode = 200;
            }
            catch (ArgumentException)
            {
                response.StatusCode = 404;
            }
            catch (Exception e)
            {
                response.StatusCode = 500;
            }
            return response;
        }
        protected async Task<BasicHttpResponse> Post(string path, string body)
        {
            return new BasicHttpResponse() { StatusCode = 501 };//not implemented
        }
        protected async Task<BasicHttpResponse> Put(string path, string body)
        {
            return new BasicHttpResponse() { StatusCode = 501 };//not implemented
        }
        protected async Task<BasicHttpResponse> Patch(string path, string body)
        {
            return new BasicHttpResponse() { StatusCode = 501 };//not implemented
        }
        protected async Task<BasicHttpResponse> Delete(string path)
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
        public async Task ProcessRequest(HttpContext context)
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
            BasicHttpResponse response = await AggregateMethod(context.Request.Method, requestPath, requestBody);

            //set the status code of the response
            context.Response.StatusCode = response.StatusCode;   

            //this needs to go last!
            await context.Response.WriteAsync(response.Body);
        }
        public async Task<BasicHttpResponse> AggregateMethod(string method, string requestPath, string requestBody)
        {
            switch (method)
            {
                case "GET":
                    return await Get(requestPath);
                case "POST":
                    return await Post(requestPath, requestBody);
                case "PUT":
                    return await Put(requestPath, requestBody);
                case "PATCH":
                    return await Patch(requestPath, requestBody);
                case "DELETE":
                    return await Delete(requestPath);
                default:
                    return new BasicHttpResponse() { StatusCode = 503 };//Is this the right status code?
            }
        }
    }
}
