using BeatSaberPlaylistsLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SongPlayListEditer.Models
{
    public class OfficialSongEntity : PlaylistSong
    {
        public new void AddIdentifierFlag(Identifier identifier) => base.AddIdentifierFlag(identifier);

        public override bool Equals(IPlaylistSong other)
        {
            return this.LevelId == other.LevelId;
        }
    }
}
