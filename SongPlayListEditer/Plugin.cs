using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Installer;
using SongPlayListEditer.Models;
using System.IO;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace SongPlayListEditer
{

    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin instance { get; private set; }
        internal static string Name => "SongPlayListEditer";

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        [Init]
        public void InitWithConfig(IPA.Config.Config conf, IPALogger logger, Zenjector zenjector)
        {
            instance = this;
            Logger.Log = logger;
            Logger.Log.Debug("Logger initialized.");
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Logger.Log.Debug("Config loaded");
            zenjector.OnMenu<PlaylistEditerInstaller>();
        }
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Logger.Info($"OnApplicationStart : Version {Assembly.GetExecutingAssembly().GetName().Version}");
            if (PluginConfig.Instance?.AutoBackup == true) {
                BackupManager.Backup(PluginConfig.Instance?.BackupPath);
            }

            if (!Directory.Exists(PluginConfig.Instance.CoverDirectoryPath)) {
                Logger.Info($"Create CoverFileDirectory : {PluginConfig.Instance.CoverDirectoryPath}");
                Directory.CreateDirectory(PluginConfig.Instance.CoverDirectoryPath);
            }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            if (PluginConfig.Instance?.AutoBackup == true) {
                BackupManager.Backup(PluginConfig.Instance?.BackupPath);
            }
            Logger.Log.Debug("OnApplicationQuit");
        }
    }
}
