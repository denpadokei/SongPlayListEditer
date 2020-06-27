using BeatSaverSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongPlayListEditer.DataBases
{
    public static class BeatSarverData
    {
        private static BeatSaver BeatSaver { get; } = new BeatSaver();

        public static async Task<string> GetBeatMapKey(string hash)
        {
            var beatmap = await BeatSaver.Hash(hash);
            return beatmap.Key;
        }
    }
}
