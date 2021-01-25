using BeatSaberPlaylistsLib.Blist;
using BeatSaberPlaylistsLib.Legacy;
using SongCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SongPlayListEditer.Utilites
{
    public static class PlaylistLibUtility
    {
        public static readonly WaitWhile _waitSongloaderLoading = new WaitWhile(() => !Loader.AreSongsLoaded || Loader.AreSongsLoading);
        public static BeatSaberPlaylistsLib.PlaylistManager CurrentManager => PlaylistManager.Utilities.PlaylistLibUtils.playlistManager;

        public static IEnumerator RefreshPlaylists()
        {
            yield return _waitSongloaderLoading;
            CurrentManager.RequestRefresh(Plugin.Name);
        }

        static PlaylistLibUtility()
        {
            BeatSaberPlaylistsLib.PlaylistManager.DefaultManager.PlaylistsRefreshRequested += DefaultManager_PlaylistsRefreshRequested;
            foreach (var manager in BeatSaberPlaylistsLib.PlaylistManager.DefaultManager.GetChildManagers()) {
                manager.PlaylistsRefreshRequested += DefaultManager_PlaylistsRefreshRequested;
            }
        }

        private static void DefaultManager_PlaylistsRefreshRequested(object sender, string e)
        {
            if (e == Plugin.Name) {
                return;
            }
            HMMainThreadDispatcher.instance.Enqueue(RefreshPlaylists());
        }
    }
}
