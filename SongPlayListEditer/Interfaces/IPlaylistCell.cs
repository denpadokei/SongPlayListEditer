using SongPlayListEditer.Statics;

namespace SongPlayListEditer.Interfaces
{
    public interface IPlaylistCell
    {
        string Title { get; set; }
        BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist { get; set; }
        SongTypeMode SongType { get; set; }
        void PostParse();
        void SelectedCell();
    }
}
