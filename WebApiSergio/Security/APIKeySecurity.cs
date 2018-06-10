using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiSergio.Security
{
   public class APIKeySecurity : DelegatingHandler
    {
        //set a default API key 
        private const string yourApiKey = "X-some-key";
 
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool keyConfere = false;
            IEnumerable<string> headers;

            //Validate that the api key exists 
            bool checkApiKeyExiste = request.Headers.TryGetValues("API_KEY", out headers);
 
            if (checkApiKeyExiste)
            {
                if (headers.FirstOrDefault().Equals(yourApiKey))
                {
                    keyConfere = true;
                }                
            }
                 
            //If the key is not valid, return an http status code.
            if (!keyConfere)
                return request.CreateResponse(HttpStatusCode.Forbidden, "Bad API Key");
 
            //Allow the request to process further down the pipeline
            var response = await base.SendAsync(request, cancellationToken);
 
            //Return the response back up the chain
            return response;
        }
    }
}