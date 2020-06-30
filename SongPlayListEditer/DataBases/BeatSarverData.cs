using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.DataBases
{
    public static class BeatSarverData
    {
        private static BeatSaver BeatSaver { get; }

        static BeatSarverData()
        {
            var http = new HttpOptions() { ApplicationName = "SongPlaylistEditer", Version = Assembly.GetExecutingAssembly().GetName().Version, Timeout = new TimeSpan(0, 0, 30) };

            BeatSaver = new BeatSaver(http);
        }

        public static async Task<string> GetBeatMapKey(string hash)
        {
            var beatmap = await BeatSaver.Hash(hash);
            return beatmap.Key;
        }
    }
}
