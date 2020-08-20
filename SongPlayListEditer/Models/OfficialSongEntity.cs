using BeatSaberPlaylistsLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Models
{
    class OfficialSongEntity : IPlaylistSong
    {
        public string Hash { get; set; }
        public string LevelId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string LevelAuthorName { get; set; }

        public Identifier Identifiers { get; set; }
        public DateTime? DateAdded { get; set; }

        public bool Equals(IPlaylistSong other)
        {
            throw new NotImplementedException();
        }
    }
}
