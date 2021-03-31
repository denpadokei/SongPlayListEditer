using BeatSaberPlaylistsLib.Types;

namespace SongPlayListEditer.Models
{
    public class OfficialSongEntity : PlaylistSong
    {
        public new void AddIdentifierFlag(Identifier identifier) => base.AddIdentifierFlag(identifier);

        public override bool Equals(IPlaylistSong other) => this.LevelId == other.LevelId;
    }
}
