using System.Net.Http.Headers;
using System.Web.Http;

namespace prjToolist {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.EnableCors(); // Cors 必加
            // Web API 設定和服務
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                "UserApiID",
                "api/{controller}/get_id/{id}",
                new {id = RouteParameter.Optional}
            );
            /* swagger API錯誤原因：此routeTemplate尚未存在。
            config.Routes.MapHttpRoute(

                  name: "DefaultApi1",
                  routeTemplate: "api/{controller}/get_hot_tags/{count}/{page}",
                  defaults: new
                  {
                      count = RouteParameter.Optional,
                      page = RouteParameter.Optional
                  }
              );
            */
        }
    }
}