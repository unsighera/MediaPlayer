﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaPlayer.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MediaPlayerEntities : DbContext
    {
        public MediaPlayerEntities()
            : base("name=MediaPlayerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
