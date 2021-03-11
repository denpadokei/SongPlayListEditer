using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.Settings;
using IPA.Loader;
using SongPlayListEditer.UI.Views;
using System;
using UnityEngine;
using Zenject;
using BeatSaberUI = BeatSaberMarkupLanguage.BeatSaberUI;

namespace SongPlayListEditer.UI
{
    /// <summary>
    /// UIまわりを管理するクラス
    /// </summary>
    public class MenuUI : MonoBehaviour, IInitializable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        public static bool IsInstalledSongBrowser { get; private set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        /// <summary>
        /// UI全般を作成します。
        /// </summary>
        public void Initialize()
        {
            Logger.Info("Start Initialize");
            this.CreateMenuButton();
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
                if (this._playlistEditorFlowCoordinator == null) {
                    this._playlistEditorFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<PlaylistEditorFlowCoordinator>();
                }
                BeatSaberMarkupLanguage.BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_playlistEditorFlowCoordinator);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }


        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistEditorFlowCoordinator _playlistEditorFlowCoordinator;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private void Constroctor()
        {
            IsInstalledSongBrowser = PluginManager.GetPlugin("Song Browser") != null;
        }
        #endregion
    }
}
