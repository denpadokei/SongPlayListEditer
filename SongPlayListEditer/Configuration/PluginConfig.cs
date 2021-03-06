﻿using IPA.Config.Stores;
using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SongPlayListEditer.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        //public virtual int IntValue { get; set; } = 42; // Must be 'virtual' if you want BSIPA to detect a value change and save the config automatically.

        public virtual string CoverDirectoryPath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "UserData", "SongPlaylistEditer", "Covers");

        public virtual int ButtonIndex { get; set; } = 0;

        public virtual bool IsSaveWithKey { get; set; } = false;

        public virtual bool AutoRefresh { get; set; } = true;

        public virtual bool AutoBackup { get; set; } = true;

        public virtual string BackupPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BeatSaber");

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other

            this.CoverDirectoryPath = other.CoverDirectoryPath;
            this.ButtonIndex = other.ButtonIndex;
            this.IsSaveWithKey = other.IsSaveWithKey;
            this.AutoRefresh = other.AutoRefresh;
            this.AutoBackup = other.AutoBackup;
        }
    }
}