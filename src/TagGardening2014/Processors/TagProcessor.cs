namespace TagGardening2014.Web.Processors
{
   using System.Collections.Generic;
   using System.Linq;
   using System.Web;
   using Globals;
   using Handlers;
   using Helpers;
   using NHunspell;

   public static class TagProcessor
   {
      private static readonly string AffFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.AffFilePath;

      private static readonly string DictionaryFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.DictionaryFilePath;

      private static readonly string DatFilePath = HttpContext.Current.Request.PhysicalApplicationPath + Settings.DatFilePath;

      private static bool SpellCheck(string word)
      {
         bool result;
         using (var hunspell = new Hunspell(AffFilePath, DictionaryFilePath))
         {
            result = hunspell.Spell(word);
         }

         return result;
      }

      private static List<string> Synonyms(string word)
      {
         var result = new List<string>();
         var thes = new MyThes(DatFilePath);
         using (var hunspell = new Hunspell(AffFilePath, DictionaryFilePath))
         {
            var tr = thes.Lookup(word, hunspell);
            foreach (var meaning in tr.Meanings)
            {
               // Betekenis
               foreach (var synonym in meaning.Synonyms)
               {
                  // Synoniem
                  result.Add(synonym);
               }
            }
         }

         return result;
      }

      private static List<string> Suggestions(string word)
      {
         List<string> result;
         using (var hunspell = new Hunspell(AffFilePath, DictionaryFilePath))
         {
            result = hunspell.Suggest(word);
         }

         return result;
      }

      public static List<WordProcessResult> GetSpellCheckForTags(List<TagSimple> tagSet)
      {
         var resultSet = new List<WordProcessResult>();
         
         foreach (var tag in tagSet)
         {
            tag.TagValue.Split(' ').ToList().ForEach(w => resultSet.Add(ProcessWord(w, tag.TagId)));
         }

         return resultSet;
      }

      private static WordProcessResult ProcessWord(string word, int tagId)
      {
         if (!SpellCheck(word))
         {
            var suggestions = Suggestions(word);
            return new WordProcessResult { TagId = tagId, Word = word, IsCorrect = false, Suggestions = suggestions };
         }
         else
         {
            return new WordProcessResult { TagId = tagId, Word = word, IsCorrect = true };
         }
      }
   }
}