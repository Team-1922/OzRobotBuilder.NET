using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework
{
    /// <summary>
    /// a class for handling platform-specific implementations of HTTP requests (i.e. AspNetCore vs AspNet)
    /// </summary>
    public static class PlatformSpecific
    {
        /// <summary>
        /// a helper class used to package the delegator with the request
        /// </summary>
        private class RequestDelegateHelper
        {
            public RequestDelegateHelper(IRequestDelegator delegator)
            {
                _delegator = delegator;
            }
            public async Task HttpRequest(HttpContext context)
            {
                await PlatformSpecific.HttpRequest(context, _delegator);
            }
            private IRequestDelegator _delegator;
        }

        /// <summary>
        /// Used in "app.Run(...)" to get a delegate with the given delegator
        /// </summary>
        /// <param name="delegator"></param>
        /// <returns></returns>
        public static RequestDelegate GetDelegate(IRequestDelegator delegator)
        {
            RequestDelegateHelper delegateHelper = new RequestDelegateHelper(delegator);
            return new RequestDelegate(delegateHelper.HttpRequest);
        }

        /// <summary>
        /// Converts the given context into standardized format
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="delegator">the delegator to execute with</param>
        /// <returns></returns>
        private static async Task HttpRequest(HttpContext context, IRequestDelegator delegator)
        {
            //get the body
            var requestBodyStream = new StreamReader(context.Request.Body);
            var requestBody = await requestBodyStream.ReadToEndAsync();

            //call the delegator
            var response = await delegator.ProcessRequestAsync(context.Request.Method, context.Request.Path, requestBody);

            //write the response
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsync(response.Body ?? "");
        }
    }
}
