namespace TagGardening2014.Web.Helpers
{
   using System.Collections.Generic;

   public class GroupedTagResult
   {
      public int GroupId { get; set; }

      public List<TagProcessResult> TagProcessResultList { get; set; }
   }
}