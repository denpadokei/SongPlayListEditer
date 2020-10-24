using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using SongPlayListEditer.UI;
using BS_Utils.Utilities;
using SongPlayListEditer.BeatSaberCommon;
using SongCore;
using System.Reflection;
using System.IO;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Models;
using SiraUtil.Zenject;
using SongPlayListEditer.Installer;

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
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Logger.log.Debug("Config loaded");
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
            Logger.log.Debug("OnApplicationQuit");
        }
    }
}
