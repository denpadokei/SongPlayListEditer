using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Extentions
{
    static class PreviewBeatMapExtention
    {
        public static string GetBeatmapHash(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                return beatmapLevel.levelID.Split('_').Last();
            }
            catch (Exception e) {
                Logger.Error(e);
                return "";
            }
        }

        public static bool IsWip(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                var hash = beatmapLevel.GetBeatmapHash();
                return hash.Split(' ').Last() == "WIP";
            }
            catch (Exception e) {
                Logger.Error(e);
                return false;
            }
        }
    }
}
