using System;
using System.Runtime.CompilerServices;
using IPALogger = IPA.Logging.Logger;

namespace SongPlayListEditer
{
    internal static class Logger
    {
        internal static IPALogger log { get; set; }

        public static void Info(string log, [CallerFilePath]string filepath = "", [CallerMemberName]string member = "", [CallerLineNumber]int? linenum = 0)
        {
#if DEBUG
            Logger.log.Info($"[{filepath}] [{member}:({linenum})] : {log}");
#else
            Logger.log.Info($"[{member}:({linenum})] : {log}");
#endif
        }
        public static void Error(string log, [CallerFilePath]string filepath = "", [CallerMemberName]string member = "", [CallerLineNumber]int? linenum = 0)
        {
#if DEBUG
            Logger.log.Error($"[{filepath}] [{member}:({linenum})] : {log}");
#else
            Logger.log.Error($"[{member}:({linenum})] : {log}");
#endif
        }

        public static void Error(Exception e, [CallerFilePath]string filepath = "", [CallerMemberName]string member = "", [CallerLineNumber]int? linenum = 0)
        {
#if DEBUG
            Logger.log.Error($"[{filepath}] [{member}:({linenum})] : {e}\r\n{e.Message}");
#else
            Logger.log.Error($"[{member}:({linenum})] : {e}\r\n{e.Message}");
#endif
        }

        public static void Debug(string log, [CallerFilePath]string filepath = "", [CallerMemberName]string member = "", [CallerLineNumber]int? linenum = 0)
        {
#if DEBUG
            Logger.log.Debug($"[{filepath}] [{member}:({linenum})] : {log}");
#endif
        }
    }
}
