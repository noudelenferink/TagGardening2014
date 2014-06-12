﻿namespace TagGardening2014.Web.App_Start
{
   using System.Web.Http;

   public static class WebApiConfig
   {
      public static void Register(HttpConfiguration config)
      {
         config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/{controller}/{action}/{mediaItemID}/{mediaTagID}",
             defaults: new
             {
                mediaItemID = RouteParameter.Optional,
                mediaTagID = RouteParameter.Optional,
             }
         );

         var json = config.Formatters.JsonFormatter;
         json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
         config.Formatters.Remove(config.Formatters.XmlFormatter);
      }
   }
}