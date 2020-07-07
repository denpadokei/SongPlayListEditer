using Newtonsoft.Json;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace SongPlayListEditer.Models
{
    public class Playlist
    {
        public string playlistTitle { get; set; }
        public string playlistAuthor { get; set; }
        public string playlistDescription { get; set; }
        public string image { get; set; }
        public int playlistSongCount { get; set; }
        public List<PlaylistSong> songs { get; set; }
        public string fileLoc { get; set; }
        public string customDetailUrl { get; set; }
        public string customArchiveUrl { get; set; }

        [NonSerialized]
        public Sprite icon;

        [JsonConstructor]
        public Playlist()
        {

        }

        public Playlist(JSONNode playlistNode)
        {
            image = playlistNode["image"].Value;
            // If we cannot find an image or parse the provided one correctly, fall back to anything.
            // It will never be displayed by SongBrowser.
            if (!string.IsNullOrEmpty(image)) {
                try {
                    icon = Base64Sprites.Base64ToSprite(image.Substring(image.IndexOf(",") + 1));
                }
                catch {
                    Logger.Error("Unable to convert playlist image to sprite!");
                    icon = Base64Sprites.StarFullIcon;
                }
            }
            else {
                icon = Base64Sprites.StarFullIcon;
            }
            playlistTitle = playlistNode["playlistTitle"];
            playlistAuthor = playlistNode["playlistAuthor"];
            customDetailUrl = playlistNode["customDetailUrl"];
            customArchiveUrl = playlistNode["customArchiveUrl"];
            if (!string.IsNullOrEmpty(customDetailUrl)) {
                if (!customDetailUrl.EndsWith("/"))
                    customDetailUrl += "/";
                Logger.Info("Found playlist with customDetailUrl! Name: " + playlistTitle + ", CustomDetailUrl: " + customDetailUrl);
            }
            if (!string.IsNullOrEmpty(customArchiveUrl) && customArchiveUrl.Contains("[KEY]")) {
                Logger.Info("Found playlist with customArchiveUrl! Name: " + playlistTitle + ", CustomArchiveUrl: " + customArchiveUrl);
            }

            songs = new List<PlaylistSong>();

            foreach (JSONNode node in playlistNode["songs"].AsArray) {
                PlaylistSong song = new PlaylistSong();
                song.key = node["key"];
                song.songName = node["songName"];
                song.hash = node["hash"];
                song.levelId = node["levelId"];

                songs.Add(song);
            }

            if (playlistNode["playlistSongCount"] != null) {
                playlistSongCount = playlistNode["playlistSongCount"].AsInt;
            }
            if (playlistNode["fileLoc"] != null)
                fileLoc = playlistNode["fileLoc"];

            if (playlistNode["playlistURL"] != null)
                fileLoc = playlistNode["playlistURL"];
        }

        public static Playlist LoadPlaylist(string path)
        {
            Logger.Info($"Path : {path}");
            var start = new Stopwatch();
            start.Start();
            try {
                var playlist = JsonConvert.DeserializeObject<Playlist>(File.ReadAllText(path));
                playlist.fileLoc = path;
                return playlist;
            }
            catch (Exception e) {
                Logger.Error(e);
                return null;
            }
            finally {
                start.Stop();
                Logger.Info($"Load time {start.ElapsedMilliseconds}ms");
            }
        }

        public bool PlaylistEqual(object obj)
        {
            if (obj == null) return false;

            var playlist = obj as Playlist;

            if (playlist == null) return false;

            int songCountThis = (songs != null ? (songs.Count > 0 ? songs.Count : playlistSongCount) : playlistSongCount);
            int songCountObj = (playlist.songs != null ? (playlist.songs.Count > 0 ? playlist.songs.Count : playlist.playlistSongCount) : playlist.playlistSongCount);

            return playlistTitle == playlist.playlistTitle &&
                   playlistAuthor == playlist.playlistAuthor &&
                   songCountThis == songCountObj;
        }

        public void CreateNew(String fileLoc)
        {
            File.WriteAllText(fileLoc, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
