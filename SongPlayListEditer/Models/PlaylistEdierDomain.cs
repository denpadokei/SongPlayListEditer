using SongPlayListEditer.Utilites;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
                PlaylistLibUtility.CurrentManager.StorePlaylist(playlist);
            });
            start.Stop();
            Logger.Info($"Save time : {start.ElapsedMilliseconds}ms");
        }
    }
}
