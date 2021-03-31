using BeatSaverSharp;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SongPlayListEditer.DataBases
{
    public static class BeatSarverData
    {
        private static BeatSaver BeatSaver { get; }

        static BeatSarverData()
        {
            var http = new HttpOptions("SongPlayListEditer", Assembly.GetExecutingAssembly().GetName().Version, new TimeSpan(0, 0, 30));

            BeatSaver = new BeatSaver(http);
        }

        public static async Task<string> GetBeatMapKey(string hash)
        {
            try {
                var beatmap = await BeatSaver.Hash(hash);
                return beatmap.Key;
            }
            catch (Exception e) {
                Logger.Error(e);
                return string.Empty;
            }
        }
    }
}
