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

namespace SongPlayListEditer.UI
{
    /// <summary>
    /// UIまわりを管理するクラス
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        public static MenuUI Instance { get; private set; }

        public LevelCollectionViewController LevelCollectionViewController { get; private set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public static void OnLoad()
        {

        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void HandleSoloModeSelection()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// インスタンス作成時に1回だけ呼ばれます。（ものびへが継承されてると勝手に呼ばれる不思議なメソッド）
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// UI全般を作成します。
        /// </summary>
        public void CreateUI()
        {
            Logger.Info("Start Create UI");
            //this.CreateMenuButton();
            this.CreateButton();
            //BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent -= this.LevelCollectionViewController_didSelectLevelEvent;
            //BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent += this.LevelCollectionViewController_didSelectLevelEvent;
            Logger.Info("Finish Create UI");
        }

        private async void LevelCollectionViewController_didSelectLevelEvent(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            await Task.Delay(5000);
            this.ShowSimplePlaylistFlowCoordinator();
        }

        /// <summary>
        /// 左側のメニューボタンを作成します。
        /// </summary>
        private void CreateMenuButton()
        {
            var button = new MenuButton("SONG PLAYLIST EDITER", "Edit song playlist", this.ShowSimplePlaylistFlowCoordinator, true);
            MenuButtons.instance.RegisterButton(button);
        }

        /// <summary>
        /// 曲選択画面内にだすボタンを作成します。
        /// </summary>
        private void CreateButton()
        {
            try {
                Logger.Info("Create Playlist Button");
                SimplePlayListView.instance.Setup();
                Logger.Info("Created Playlist button!");

                //if (BeatSaberUtility.LevelCollectionViewController != null) {
                //    Logger.Info("Create Playlist Button");
                //    SimplePlayListView.instance.Setup();


                //    _playlistButton = BeatSaberUtility.CreateUIButton(BeatSaberUtility.PlayButtons, BeatSaberUtility.PlayButton);
                //    _playlistButton.onClick.AddListener(delegate ()
                //    {
                //        Logger.Info("Click Playlist Button");
                //        this.ShowSimplePlaylistFlowCoordinator();
                //    });
                //    _playlistButton.name = "PLAY LIST";
                //    _playlistButton = BeatSaberUtility.LevelDetailViewController.CreateUIButton("ApplyButton",
                //        new Vector2(-20f, 32f),
                //        new Vector2(9f, 5.5f),
                //        () =>
                //        {

                //        },
                //        "Playlists");

                //    (_playlistButton.transform as RectTransform).anchorMin = new Vector2(1, 1);
                //    (_playlistButton.transform as RectTransform).anchorMax = new Vector2(1, 1);

                //    _playlistButton.ToggleWordWrapping(false);
                //    _playlistButton.SetButtonTextSize(2.0f);
                //    BeatSaberUtility.DestroyHoverHint(_playlistButton.transform as RectTransform);

                //    _playlistButton.name = "PlaylistEditButton";
                //    BeatSaberUtility.AddHintText(_playlistButton.transform as RectTransform, "Edit Playlist");
                //    Logger.Info("Created Playlist button!");
                //}
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
            if (this._mainFlowCoordinater == null) {
                this._mainFlowCoordinater = BeatSaberUI.CreateFlowCoordinator<MainFlowCoordinator>();
            }

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this._mainFlowCoordinater);
        }

        private void ShowSimplePlaylistFlowCoordinator()
        {
            try {
                Logger.Info("Click Playlist Button");

                if (this._simpleFlowCoordinater == null) {
                    this._simpleFlowCoordinater = BeatSaberUI.CreateFlowCoordinator<SimpleFlowCoordinater>();
                }

                //BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this._simpleFlowCoordinater);

                var soloFlow = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().FirstOrDefault();
                soloFlow?.InvokeMethod<object, SoloFreePlayFlowCoordinator>("PresentFlowCoordinator", this._simpleFlowCoordinater, null, false, false);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private MainFlowCoordinator _mainFlowCoordinater;

        private SimpleFlowCoordinater _simpleFlowCoordinater;

        private Button _playlistButton;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
