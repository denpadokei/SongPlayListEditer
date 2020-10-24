using SongPlayListEditer.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Interfaces
{
    public interface IPlaylistCell
    {
        string Title { get; set; }
        BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist { get; set; }
        SongTypeMode SongType { get; set; }
        void PostParse();
        void SetPlaylistInformation(BeatSaberPlaylistsLib.Types.IPlaylist playlists, IPreviewBeatmapLevel beatmapLevel);
    }
}
