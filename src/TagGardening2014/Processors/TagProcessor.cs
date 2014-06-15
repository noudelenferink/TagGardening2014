namespace TagGardening2014.Web.Processors
{
   using System.Collections.Generic;
   using System.Linq;
   using System.Web;
   using Globals;
   using Handlers;
   using Helpers;
   using Model;
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
            var stemmedWordResult = hunspell.Stem(word);
            if (stemmedWordResult.Any())
            {
               var stemmedWord = stemmedWordResult.FirstOrDefault();
               if (!string.IsNullOrEmpty(stemmedWord))
               {
                  var thesaurusResult = thes.Lookup(stemmedWord);
                  if (thesaurusResult != null && thesaurusResult.Meanings != null && thesaurusResult.Meanings.Any())
                  {
                     thesaurusResult.Meanings.ForEach(m => m.Synonyms
                        .Where(s => s.ToLower() != stemmedWord.ToLower())
                        .Where(s => s.ToLower() != word.ToLower())
                        .ToList()
                        .ForEach(s => result.Add(s.ToLower()))
                     );
                  }
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

      public static List<TagProcessResult> GetSpellCheckForTags(List<TagSimple> tagSet)
      {
         var resultSet = new List<WordProcessResult>();
         
         foreach (var tag in tagSet)
         {
            tag.TagValue.Split(' ').ToList().ForEach(w => resultSet.Add(ProcessWord(w, tag.TagId)));
         }
         var groupedResultSet = resultSet.GroupBy(r => r.TagId).Select(i => new TagProcessResult{ TagId = i.Key, WordProcessResultList = i.ToList()}).ToList();
         return groupedResultSet;
      }

      private static WordProcessResult ProcessWord(string word, int tagId)
      {
         if (!SpellCheck(word))
         {
            var suggestions = Suggestions(word);
            return new WordProcessResult { TagId = tagId, Word = word, Skip = true, Suggestions = suggestions };
         }

         return new WordProcessResult { TagId = tagId, Word = word, Skip = false };
      }

      public static List<TagProcessResult> ProcessTagSet(List<TagSimple> tagSet)
      {
         var resultList = new List<TagProcessResult>();

         // Spellcheck
         tagSet.ForEach(t => resultList.Add(
            new TagProcessResult
            {
               TagId = t.TagId, 
               TagValue = t.TagValue,
               WordProcessResultList = t.TagValue.Split(' ').ToList().Select(w => ProcessWord(w, t.TagId)).ToList()
            }));

         var correctlySpelledTagList = resultList.Where(t => t.WordProcessResultList.All(w => !w.Skip)).ToList();

         resultList.Where(t => t.WordProcessResultList.All(w => !w.Skip)).ToList().ForEach(tpr => CheckTagSetTest(correctlySpelledTagList, tpr));


         //// Check all tags that are 100% spelled correctly for a duplicate in provided set.
         //resultList.Where(t => t.WordProcessResultList.All(w => !w.Skip)).ToList().ForEach(tpr => CheckTagSetForDuplicates(correctlySpelledTagList, tpr));
         
         //// Check all tags that are unique in the provided set of tags and are 100% spelled correctly for a duplicate in the media tag repository.
         //resultList.Where(t => !t.DuplicateTagId.HasValue && t.WordProcessResultList.All(w => !w.Skip)).ToList().ForEach(CheckRepositoryForDuplicates);

         
         //resultList.Where(t => !t.MediaTagId.HasValue).ToList().ForEach(t => CheckTagSetForSynonyms(correctlySpelledTagList, t));

         return resultList;
      }

      private static void CheckRepositoryForDuplicates(TagProcessResult tpr)
      {
         using (var context = new TagGardeningContext())
         {
            var repoMediaTagId =
               context.MediaTags.Where(mt => mt.MediaTagValue.ToLower() == tpr.TagValue.ToLower())
                                .Select(mt => mt.MediaTagId)
                                .SingleOrDefault();
            if (repoMediaTagId != default(int))
            {
               tpr.MediaTagId = repoMediaTagId;
            }
         }
      }

      private static void CheckTagSetForDuplicates(List<TagProcessResult> tagList, TagProcessResult tpr)
      {
         var duplicateTagId = 
            tagList.Where(t => t.TagId != tpr.TagId)
                   .Where(t => !t.DuplicateTagId.HasValue)
                   .Where(t => t.TagValue.ToLower() == tpr.TagValue.ToLower())
                   .Select(t => t.TagId).FirstOrDefault();
         if (duplicateTagId != default(int))
         {
            tpr.DuplicateTagId = duplicateTagId;
         }
      }

      private static void CheckTagSetForSynonyms(List<TagProcessResult> tagList, TagProcessResult tpr)
      {
         var synonymTagId = 0;
         foreach (var wpr in tpr.WordProcessResultList)
         {
            var wordSynonyms = Synonyms(wpr.Word);
            if (wordSynonyms.Any())
            {
               synonymTagId = 
                  tagList.Where(t => t.TagId != tpr.TagId)
                         .Where(t => !t.SynonymTagId.HasValue)
                         .Where(t => t.WordProcessResultList.Any(x => wordSynonyms.Contains(x.Word)))
                         .Select(x => x.TagId)
                         .FirstOrDefault();
            }
         }

         if (synonymTagId != default(int))
         {
            tpr.SynonymTagId = synonymTagId;
         }
      }

      private static void CheckTagSetTest(List<TagProcessResult> tagList, TagProcessResult tagToCheck)
      {
         var tagWordSynonyms = GetSynonymsForTagWords(tagToCheck);
         var result = (from x in tagList
                      where x.TagId != tagToCheck.TagId
                      where !x.DuplicateTagId.HasValue
                      where !x.RelatedTagId.HasValue
                      let foundDuplicateTagId = !tagToCheck.IsDuplicateReference && x.TagValue.ToLower() == tagToCheck.TagValue.ToLower() ? x.TagId : (int?)null
                      let foundPartialDuplicateTagId = !tagToCheck.IsRelatedReference && x.WordProcessResultList.Any(a => tagToCheck.WordProcessResultList.Select(wpr => wpr.Word.ToLower()).Contains(a.Word.ToLower())) ? x.TagId : (int?)null
                      let foundSynonymTagId = !tagToCheck.IsRelatedReference && x.WordProcessResultList.Any(w => tagWordSynonyms.Contains(w.Word.ToLower())) ? x.TagId : (int?)null
                      select new
                      {
                        DuplicateTagId = foundDuplicateTagId,
                        RelatedTagId = foundPartialDuplicateTagId ?? foundSynonymTagId
                      }).FirstOrDefault();

         if (result != null)
         {
            if (result.DuplicateTagId.HasValue)
            {
               tagToCheck.DuplicateTagId = result.DuplicateTagId;
               tagList.Find(t => t.TagId == result.DuplicateTagId).IsDuplicateReference = true;
            }

            if (result.RelatedTagId.HasValue)
            {
               tagToCheck.RelatedTagId = result.RelatedTagId;
               tagList.Find(t => t.TagId == result.RelatedTagId).IsRelatedReference = true;
            }
         }
      }

      private static List<string> GetSynonymsForTagWords(TagProcessResult tag)
      {
         var returnList = new List<string>();
         tag.WordProcessResultList.ForEach(w => returnList.AddRange(Synonyms(w.Word)));
         return returnList;
      }
   }
}