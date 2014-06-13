namespace TagGardening2014.Web.Helpers
{
   using System.Collections.Generic;

   public class TagProcessResult
   {
      public int TagId { get; set; }
      public List<WordProcessResult> WordProcessResultList { get; set; }
   }
}