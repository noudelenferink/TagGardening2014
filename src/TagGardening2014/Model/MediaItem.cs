//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TagGardening2014.Web.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MediaItem
    {
        public MediaItem()
        {
            this.MediaTags = new HashSet<MediaItemMediaTag>();
            this.Tags = new HashSet<Tag>();
        }
    
        public int MediaItemId { get; set; }
        public int MediaTypeId { get; set; }
        public int MediaItemStatusId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string ThumbFileName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
    
        public virtual MediaItemStatus MediaItemStatus { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<MediaItemMediaTag> MediaTags { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
