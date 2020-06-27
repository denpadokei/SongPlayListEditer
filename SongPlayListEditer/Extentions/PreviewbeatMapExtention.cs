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
                return beatmapLevel.levelID.Split('_')[2];
            }
            catch (Exception e) {
                Logger.Error(e);
                return "";
            }
        }
    }
}
