using BeatSaberPlaylistsLib;
using IPA.Utilities;
using Newtonsoft.Json;
using SongPlayListEditer.DataBases;
using SongPlayListEditer.Statics;
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
    public class PlaylistEdierDomain
    {
        public async Task SavePlaylist(BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            if (string.IsNullOrEmpty(playlist.Filename)) {
                playlist.Filename = $"{playlist.Title}_{DateTime.Now:yyyyMMddHHmmss}";
            }
            var start = new Stopwatch();
            start.Start();
            await Task.Run(() =>
            {
                Logger.Info($"Filename : {playlist.Filename}");
                PlaylistManager.DefaultManager.StorePlaylist(playlist);
            });
            start.Stop();
            Logger.Info($"Save time : {start.ElapsedMilliseconds}ms");
        }
    }
}
