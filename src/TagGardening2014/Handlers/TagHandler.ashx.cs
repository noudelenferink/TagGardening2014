namespace TagGardening2014.Web.Handlers
{
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Web;
   using System.Web.Script.Serialization;
   using Globals;
   using Helpers;
   using Processors;

   /// <summary>
   /// Summary description for TagHandler
   /// </summary>
   public class TagHandler : IHttpHandler
   {
      private readonly string affFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.AffFilePath;

      private readonly string dictionaryFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.DictionaryFilePath;

      private readonly string datFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.DatFilePath;

      public void ProcessRequest(HttpContext context)
      {
         try
         {
            context.Response.ContentType = "application/json";
            var data = context.Request;
            var sr = new StreamReader(data.InputStream);
            var stream = sr.ReadToEnd();
            var javaScriptSerializer = new JavaScriptSerializer();
            var PostedData = javaScriptSerializer.Deserialize<TagSetObject>(stream);
            //var result = TagProcessor.GetSpellCheckForTags(PostedData.TagSet);
            var test = TagProcessor.ProcessTagSet(PostedData.TagSet);
            var groupedResult = (from tpr in test
                                let identifier = tpr.DuplicateTagId ?? tpr.RelatedTagId ?? tpr.TagId
                                group tpr by identifier into groupedTagResults
                                select new GroupedTagResult
                                {
                                   GroupId = groupedTagResults.Key,
                                   TagProcessResultList = groupedTagResults.ToList()
                                }).ToList();

            context.Response.Write(javaScriptSerializer.Serialize(groupedResult));
         }
         catch (Exception msg) { context.Response.Write(msg.Message); }
      }

      public bool IsReusable
      {
         get
         {
            return false;
         }
      }
   }

   [Serializable]
   public class TagSetObject
   {
      public List<TagSimple> TagSet { get; set; }
   }

   [Serializable]
   public class TagSimple
   {
      public int TagId { get; set; }
      public string TagValue { get; set; }
   }
}