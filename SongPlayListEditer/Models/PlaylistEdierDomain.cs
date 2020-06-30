using IPA.Utilities;
using SongPlayListEditer.DataBases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SongPlayListEditer.Models
{
    public class PlaylistEdierDomain
    {
        public async Task SavePlaylist(Playlist playlist)
        {
            if (string.IsNullOrEmpty(playlist.fileLoc)) {
                playlist.fileLoc = Path.Combine(Directory.GetCurrentDirectory(), "Playlists", playlist.playlistTitle);
            }
            foreach (var songinfo in playlist.songs.Where(x => string.IsNullOrEmpty(x.key))) {
                try {
                    songinfo.key = await BeatSarverData.GetBeatMapKey(songinfo.hash);
                }
                catch (Exception e) {
                    Logger.Error(e);
                    songinfo.key = "";
                }
            }
            playlist.CreateNew(playlist.fileLoc);
        }
    }
}
