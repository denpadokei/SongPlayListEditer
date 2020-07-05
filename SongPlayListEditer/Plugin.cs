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

namespace SongPlayListEditer
{

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin instance { get; private set; }
        internal static string Name => "SongPlayListEditer";

        private MenuUI _menuUI;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            instance = this;
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        [Init]
        public void InitWithConfig(IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Logger.log.Debug("Config loaded");
        }
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Logger.Info($"OnApplicationStart : Version {Assembly.GetExecutingAssembly().GetName().Version}");
            // new GameObject("SongPlayListEditerController").AddComponent<SongPlayListEditerController>();
            if (this._menuUI == null) {
                this._menuUI = new GameObject("SongPlaylistEditerMenuUI").AddComponent<MenuUI>();
            }

            BSEvents.lateMenuSceneLoadedFresh += this.BSEvents_lateMenuSceneLoadedFresh;

            if (!Directory.Exists(PluginConfig.Instance.CoverFilePath)) {
                Logger.Info($"Create CoverFileDirectory : {PluginConfig.Instance.CoverFilePath}");
                Directory.CreateDirectory(PluginConfig.Instance.CoverFilePath);
            }
        }

        private void BSEvents_lateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            BeatSaberUtility.Initialize();
            MenuUI.Instance.CreateUI();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");

        }
    }
}
