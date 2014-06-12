namespace TagGardening2014.Web.Controllers
{
   using System.Data.Entity;
   using System.Linq;
   using System.Web.Http;
   using Breeze.ContextProvider.EF6;
   using Breeze.WebApi2;
   using Model;

   [BreezeController]
   public class TagGardeningController : ApiController
   {
      readonly EFContextProvider<TagGardeningContext> _contextProvider = new EFContextProvider<TagGardeningContext>();

      // ~/breeze/todos/Metadata 
      [HttpGet]
      public string Metadata()
      {
         return _contextProvider.Metadata();
      }

      // GET api/<controller>
      [HttpGet]
      public IQueryable<MediaItem> MediaItems()
      {
         return _contextProvider.Context.MediaItems
            .Include("Tags")
            .Include("MediaTags")
            .Include("MediaItemStatus")
            .Include(mi => mi.MediaTags.Select(mt => mt.MediaTag))
            .Include(mi => mi.MediaTags.Select(mt => mt.MediaTag.MediaTagType));
      }

      [HttpGet]
      public IQueryable<Tag> Tags()
      {
         return _contextProvider.Context.Tags;
      }
   }
}