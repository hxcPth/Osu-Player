﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Milkitic.OsuPlayer.Data
{
    [Table("map_info")]
    public class MapInfo
    {
        public MapInfo() { }

        public MapInfo(string id, string version, string folderName, int offset, DateTime? lastPlayTime,
            string exportFile = null, DateTime? addTime = null)
        {
            Id = id;
            Version = version;
            FolderName = folderName;
            Offset = offset;
            LastPlayTime = lastPlayTime;
            AddTime = addTime;
            if (exportFile != null) ExportFile = exportFile;
        }

        [Required, Column("id")]
        public string Id { get; set; }
        [Required, Column("version")]
        public string Version { get; set; }
        [Required, Column("folder")]
        public string FolderName { get; set; }
        [Column("offset")]
        public int Offset { get; set; }
        [Column("lastPlayTime")]
        public DateTime? LastPlayTime { get; set; }
         [Column("exportFile")]
        public string ExportFile { get; set; }

        //Extension
        public DateTime? AddTime { get; }

    }

    public class MapInfoContext : DbContext
    {
        public DbSet<MapInfo> MapInfos { get; set; }
        public MapInfoContext() : base("sqlite") { }
    }

    [Table("collection")]
    public class Collection
    {
        public Collection() { }

        public Collection(string id, string name, bool locked, int index)
        {
            Id = id;
            Name = name;
            LockedInt = locked ? 1 : 0;
            Index = index;
        }

        [Required, Column("id")]
        public string Id { get; set; }
        [Required, Column("name")]
        public string Name { get; set; }
        [Column("locked")]
        public int LockedInt { get; set; }
        [Column("index")]
        public int Index { get; set; }

        public bool Locked => LockedInt == 1;
    }

    public class CollectionContext : DbContext
    {
        public DbSet<Collection> Collections { get; set; }
        public CollectionContext() : base("sqlite") { }
    }

    [Table("collection_relation")]
    public class CollectionRelation
    {
        public CollectionRelation() { }

        public CollectionRelation(string id, string collectionId, string mapId)
        {
            Id = id;
            CollectionId = collectionId;
            MapId = mapId;
            AddTime = DateTime.Now;
        }

        [Required, Column("id")]
        public string Id { get; set; }
        [Required, Column("collectionId")]
        public string CollectionId { get; set; }
        [Required, Column("mapId")]
        public string MapId { get; set; }
        [Column("addTime")]
        public DateTime? AddTime { get; set; }
    }

    public class RelationContext : DbContext
    {
        public DbSet<CollectionRelation> Relations { get; set; }
        public RelationContext() : base("sqlite") { }
    }
}