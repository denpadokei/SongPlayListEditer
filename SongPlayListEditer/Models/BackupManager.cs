using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SongPlayListEditer.Models
{
    internal static class BackupManager
    {
        public static readonly string PLAYLIST_PATH = Path.Combine(Environment.CurrentDirectory, "Playlists");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetpath">Target directory path.</param>
        /// <returns></returns>
        public static bool Backup(string targetpath = null)
        {
            try {
                var path = string.IsNullOrEmpty(targetpath) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BeatSaber") : targetpath;
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                var fileInfo = new FileInfo(path);
                if (File.Exists(path)) {
                    fileInfo.CopyTo(Path.ChangeExtension(fileInfo.FullName, ".bak"), true);
                    fileInfo.Delete();
                }
                path = Path.Combine(path, $"Playlists_{DateTime.Now:yyyyMMdd}.zip");
                var dstFileInfo = new FileInfo(path);
                var files = new DirectoryInfo(PLAYLIST_PATH).EnumerateFiles("*.*", SearchOption.AllDirectories).ToList();
                using (var st = File.Create(dstFileInfo.FullName))
                using (var zip = new ZipArchive(st, ZipArchiveMode.Update)) {
                    foreach (var item in files) {
                        try {
                            zip.CreateEntryFromFile(item.FullName, item.Name);
                        }
                        catch (Exception e) {
                            Logger.Error(e);
                        }
                    }
                }

                File.Delete(Path.ChangeExtension(fileInfo.FullName, ".bak"));
            }
            catch (Exception e) {
                Logger.Error(e);
                return false;
            }
            Logger.Info("Complete back up");
            return true;
        }
    }
}
