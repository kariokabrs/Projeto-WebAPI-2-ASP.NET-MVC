using System.Web.Http;

namespace WebApiSergio
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //1 forma Para que saia o formato do Json na respota da WebAPI como padrão mas sem Indent.
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //2 forma Para que saia o formato do Json na respota da WebAPI como padrão.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Aqui para formatar o Json em Linhas com Indent.
            config.Formatters.JsonFormatter.Indent = true;

            //3 forma sem uso do Json.NET
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.UseDataContractJsonSerializer = true;
            //json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Converte todas as datas para UTC
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        }
    }
}
