using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiSergio.Security
{
    public class AuthHandler : DelegatingHandler
    {
        string _userName = "";

        private bool ValidateCredentials(AuthenticationHeaderValue authenticationHeaderVal)
        {
            try
            {
                if (authenticationHeaderVal != null && !String.IsNullOrEmpty(authenticationHeaderVal.Parameter))
                {
                    string[] decodedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(authenticationHeaderVal.Parameter)).Split(new[] { ':' });

                    if (decodedCredentials[0].Equals("username") && decodedCredentials[1].Equals("password"))
                    {
                        _userName = "John Doe";
                        return true;//request authenticated.
                    }
                }
                return false;//request not authenticated.
            }
            catch
            {
                return false;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
          
            if (ValidateCredentials(request.Headers.Authorization))
            {
                Thread.CurrentPrincipal = new TestAPIPrincipal(_userName);
                HttpContext.Current.User = new TestAPIPrincipal(_userName);
            }
        
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized
                && !response.Headers.Contains("WwwAuthenticate"))
            {
                response.Headers.Add("WwwAuthenticate", "Basic");
            }

            return response;
        }
    }
}