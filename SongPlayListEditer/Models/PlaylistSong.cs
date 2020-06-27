using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Models
{
    public class PlaylistSong
    {
        public string key { get; set; }
        public string songName { get; set; }
        public string hash { get; set; }

        [NonSerialized]
        public string levelId;
        [NonSerialized]
        public CustomPreviewBeatmapLevel level;
        [NonSerialized]
        public bool oneSaber;
        [NonSerialized]
        public string path;
    }
}
