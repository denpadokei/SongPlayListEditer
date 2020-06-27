using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Statics
{
    public class FilePathName
    {
        public static string PlaylistsFolderPath => Path.Combine(Directory.GetCurrentDirectory(), "Playlists");
    }
}
