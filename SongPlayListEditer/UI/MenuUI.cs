using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA.Logging;
using BeatSaberMarkupLanguage;
using BS_Utils.Utilities;
using UnityEngine.UI;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Extentions;

using BeatSaberUI = BeatSaberMarkupLanguage.BeatSaberUI;
using IPA.Utilities;
using SongPlayListEditer.UI.Views;
using BeatSaberMarkupLanguage.Settings;
using SongPlayListEditer.Models;
using Zenject;

namespace SongPlayListEditer.UI
{
    /// <summary>
    /// UIまわりを管理するクラス
    /// </summary>
    public class MenuUI : MonoBehaviour, IInitializable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        /// <summary>
        /// UI全般を作成します。
        /// </summary>
        [Inject]
        public void CreateUI(SimplePlayListView simplePlayListView)
        {
            Logger.Info("Start Create UI");
            this._simplePlayListView = simplePlayListView;
            Logger.Info("Finish Create UI");
        }


        public void Initialize()
        {
            Logger.Debug($"Start Initialize.");
            this._simplePlayListView.Initialize();
            this.CreateMenuButton();
            //this.CreateButton();
            this.CreateSetting();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド

        /// <summary>
        /// 左側のメニューボタンを作成します。
        /// </summary>
        private void CreateMenuButton()
        {
            var button = new MenuButton("SONG PLAYLIST EDITER", "Edit song playlist", this.ShowMainFlowCoodniator, true);
            MenuButtons.instance.RegisterButton(button);
        }

        /// <summary>
        /// 曲選択画面内にだすボタンを作成します。
        /// </summary>
        private void CreateButton()
        {
            try {
                Logger.Info("Create Playlist Button");
                //SimplePlayListView.instance.Setup();
                Logger.Info("Created Playlist button!");
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// 設定用のUIを作成します。
        /// </summary>
        private void CreateSetting()
        {
            try {
                var setting = BeatSaberUI.CreateViewController<SettingView>();
                BSMLSettings.instance.AddSettingsMenu("SONG PLAYLIST EDITER", setting.ResourceName, setting);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// プレイリスト編集画面に遷移します。
        /// </summary>
        private void ShowMainFlowCoodniator()
        {
            try {
                Logger.Info("Click Playlist Button");
                if (this._mainFlowCoordinater == null) {
                    this._mainFlowCoordinater = BeatSaberUI.CreateFlowCoordinator<PlaylistEditorFlowCoordinator>();
                }

                BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this._mainFlowCoordinater);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [Inject]
        DiContainer _container;
        [Inject]
        private PlaylistEditorFlowCoordinator _mainFlowCoordinater;
        private SimplePlayListView _simplePlayListView;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
