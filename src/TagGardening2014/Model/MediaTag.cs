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
    
    public partial class MediaTag
    {
        public MediaTag()
        {
            this.MediaItemMediaTags = new HashSet<MediaItemMediaTag>();
        }
    
        public int MediaTagId { get; set; }
        public Nullable<int> MediaTagTypeId { get; set; }
        public string MediaTagValue { get; set; }
    
        public virtual ICollection<MediaItemMediaTag> MediaItemMediaTags { get; set; }
        public virtual MediaTagType MediaTagType { get; set; }
    }
}
