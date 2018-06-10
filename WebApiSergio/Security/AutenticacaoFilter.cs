using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Text;
using System;
using System.Web.Http.Controllers;

namespace WebApiSergio.Security
{
    public class AutenticacaoFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                string AutenticacaoToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodeToken = Encoding.UTF8.GetString(Convert.FromBase64String(AutenticacaoToken));
                string usuario = decodeToken.Substring(0, decodeToken.IndexOf(":"));
                string senha = decodeToken.Substring(decodeToken.IndexOf(":") + 1);
                if(usuario == "Sergio"  && senha == "Rezende")
                {
                    //autorizado
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}