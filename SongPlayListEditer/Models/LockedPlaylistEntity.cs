using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SongPlayListEditer.Models
{
    public class LockedPlaylistEntity
    {
        [JsonIgnore]
        public static readonly string LockListFilePath = Path.Combine(Application.persistentDataPath, "LockedPlaylists.json");

        public ReadOnlyCollection<string> LockedPlaylists { get; set; }

        public LockedPlaylistEntity Add(params string[] playlistFileNames)
        {
            var tmp = new List<string>(this.LockedPlaylists);
            foreach (var fileName in playlistFileNames) {
                if (tmp.Any(x => Regex.IsMatch(x, $"^{fileName}$", RegexOptions.IgnoreCase))) {
                    continue;
                }
                tmp.Add(fileName);
            }
            this.LockedPlaylists = new ReadOnlyCollection<string>(tmp.OrderBy(x => x).ToList());
            return this;
        }

        public LockedPlaylistEntity Remove(params string[] playlistFileNames)
        {
            var tmp = new List<string>(this.LockedPlaylists);
            foreach (var fileName in playlistFileNames) {
                tmp.RemoveAll(x => Regex.IsMatch(x, $"^{fileName}$", RegexOptions.IgnoreCase));
            }
            this.LockedPlaylists = new ReadOnlyCollection<string>(tmp.OrderBy(x => x).ToList());
            return this;
        }

        public void Save()
        {
            try {
                File.WriteAllText(LockListFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        public LockedPlaylistEntity Read()
        {
            try {
                if (!File.Exists(LockListFilePath)) {
                    this.LockedPlaylists = new ReadOnlyCollection<string>(new List<string>());
                    File.WriteAllText(LockListFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
                }
                var obj = JsonConvert.DeserializeObject<LockedPlaylistEntity>(File.ReadAllText(LockListFilePath));
                this.LockedPlaylists = obj.LockedPlaylists;
            }
            catch (Exception e) {
                Logger.Error(e);
            }

            return this;
        }
    }
}
