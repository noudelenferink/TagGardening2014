namespace TagGardening2014.Web.Helpers
{
   using System.Collections.Generic;

   public class TagProcessResult
   {
      public int TagId { get; set; }

      public string TagValue { get; set; }

      public List<WordProcessResult> WordProcessResultList { get; set; }
      public int? MediaTagId { get; set; }
      public int? DuplicateTagId { get; set; }
      public int? SynonymTagId { get; set; }
      public int? RelatedTagId { get; set; }

      public bool IsDuplicateReference{ get; set; }
      public bool IsRelatedReference { get; set; }
   }
}