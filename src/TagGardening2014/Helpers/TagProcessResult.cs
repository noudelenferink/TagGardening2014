namespace TagGardening2014.Web.Helpers
{
   using System.Collections.Generic;

   public class WordProcessResult
   {
      public int TagId { get; set; }
      public string Word { get; set; }

      public bool IsCorrect { get; set; }

      public List<string> Suggestions { get; set; }
   }
}