﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TagGardeningContext : DbContext
    {
        public TagGardeningContext()
            : base("name=TagGardeningContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MediaItem> MediaItems { get; set; }
        public virtual DbSet<MediaItemMediaTag> MediaItemMediaTags { get; set; }
        public virtual DbSet<MediaItemStatus> MediaItemStatus1 { get; set; }
        public virtual DbSet<MediaTag> MediaTags { get; set; }
        public virtual DbSet<MediaTagType> MediaTagTypes { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }
}
