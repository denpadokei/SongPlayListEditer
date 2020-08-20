using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.Extentions
{
    static class PreviewBeatMapExtention
    {
        private const int LEVELID_LENGTH = 32;

        public static string GetBeatmapHash(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                return beatmapLevel.levelID.Length >= LEVELID_LENGTH ? beatmapLevel.levelID.Split('_').Last() : beatmapLevel.levelID;
            }
            catch (Exception e) {
                Logger.Error(e);
                return "";
            }
        }

        public static bool IsWip(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                return beatmapLevel.levelID.Split(' ').Last() == "WIP";
            }
            catch (Exception e) {
                Logger.Error(e);
                return false;
            }
        }

        public static bool IsOfficial(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                return beatmapLevel.levelID.Length < LEVELID_LENGTH;
            }
            catch (Exception e) {
                Logger.Error(e);
                return false;
            }
        }

        public static bool IsCustom(this IPreviewBeatmapLevel beatmapLevel)
        {
            try {
                return beatmapLevel.levelID.Length >= LEVELID_LENGTH && !beatmapLevel.IsWip();
            }
            catch (Exception e) {
                Logger.Error(e);
                return false;
            }
        }
    }
}
