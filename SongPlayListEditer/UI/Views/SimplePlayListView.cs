using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BS_Utils.Utilities;
using HMUI;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Interfaces;
using SongPlayListEditer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using PlaylistUI = SongPlayListEditer.BeatSaberCommon.PlaylistUI;

namespace SongPlayListEditer.UI.Views
{
    [HotReload]
    public class SimplePlayListView : ViewContlollerBindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>
        /// For this method of setting the ResourceName, this class must be the first class in the file. 
        /// </summary>
        public string ResourceName => string.Join(".", GetType().Namespace, "SimplePlayListView.bsml");
        [UIValue("playlists")]
        private List<object> Playlists { get; } = new List<object>();

        public IPreviewBeatmapLevel Beatmap { get; private set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void CreateList()
        {
            _ = this.CreateList(false);
        }

        public async Task CreateList(bool isAdded)
        {
            await _createlistSemaphore.WaitAsync();

            var start = new Stopwatch();
            start.Start();
            try {
                Logger.Info("Create List");
                this.Playlists.Clear();
                this._playlists.tableView.ReloadData();
                foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                    var cell = this._factory.Create(playlist, this.Beatmap);
                    Logger.Debug($"{cell}");
                    this.Playlists.Add(cell);
                }
                Logger.Info($"Playlists count : {this._playlists.data.Count}");
                this._playlists.tableView.ReloadData();
                Logger.Info("Created List");
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                start.Stop();
                Logger.Info($"Creat time : {start.ElapsedMilliseconds}ms");
                _createlistSemaphore.Release();
            }
        }

        private void LevelCollectionViewController_didSelectLevelEvent(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            this.Beatmap = arg2;
            Logger.Info($"Selected Beatmaplevel, [{arg2.songName} : {arg2.GetBeatmapHash()}]");

            if (arg2.IsCustom()) {
                this._playlistButton.interactable = true;
            }
            else if (arg2.IsWip()) {
                this._playlistButton.interactable = false;
            }
            else if (arg2.IsOfficial()) {
                this._playlistButton.interactable = true;
            }
            else {
                this._playlistButton.interactable = false;
            }

            _modal?.Hide(true);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        [Inject]
        private void Constractor(PlaylistCellEntity.CellFactory cellFactory)
        {
            this._factory = cellFactory;
        }

        public void Initialize()
        {
            try {
                standardLevelDetailViewController = container.Resolve<StandardLevelDetailViewController>();
                levelCollectionViewController = container.Resolve<LevelCollectionViewController>();
                var standardLevelDetailView = standardLevelDetailViewController.GetPrivateField<StandardLevelDetailView>("_standardLevelDetailView");
                Logger.Debug($"standardLevelDetailView is null? : {standardLevelDetailView == null}");
                var playContainer = standardLevelDetailView.GetComponentsInChildren<RectTransform>().First(x => x.name == "BeatmapParamsPanel");
                _playButtons = standardLevelDetailView.GetComponentsInChildren<RectTransform>().First(x => x.name == "ActionButtons");
                BSMLParser.instance?.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), this.rectTransform.gameObject, this);
                var button = Resources.FindObjectsOfTypeAll<Button>().First(x => x.name == "PracticeButton").transform as RectTransform;
                _playlistButton = PlaylistUI.CreateIconButton("PlaylistButton", _playButtons.transform as RectTransform, Vector2.zero, new Vector2(6f, button.sizeDelta.y), this.ShowModal, Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.round_playlist_add_white_18dp.png"), "PracticeButton"); //PlaylistUI.CreateUIButton(_playButtons as RectTransform, Resources.FindObjectsOfTypeAll<Button>().First(x => x.name == "PracticeButton"), "PLAY LIST");
                _playlistButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 2.1f;
                Logger.Debug($"Button is null? : {_playlistButton == null}");
                Logger.Debug($"modal is null? : {_modal == null}");
                this._playlistButton.transform.SetSiblingIndex(PluginConfig.Instance.ButtonIndex);
                this._playlistButton.interactable = true;
                levelCollectionViewController.didSelectLevelEvent -= this.LevelCollectionViewController_didSelectLevelEvent;
                levelCollectionViewController.didSelectLevelEvent += this.LevelCollectionViewController_didSelectLevelEvent;
                PlaylistUI.ConvertIconButton(ref this._closeButton, new Vector2(50f, 50f), Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.baseline_close_white_18dp.png"));
                _closeButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 1.2f;
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private void ShowModal()
        {
            Logger.Info($"modal scale. [x : {this._modal.transform.position.x}, y : {this._modal.transform.position.y}, z : {this._modal.transform.position.z}]");
            this._modal.transform.SetParent(this.standardLevelDetailViewController.rectTransform);
            this._modal.transform.position = _defaultLocalScale;
            this.CreateList();
            this._modal.Show(true);
        }

        [UIAction("current")]
        private void SelectedCell(TableView tableView, IPlaylistCell playlistCell)
        {
            playlistCell.SelectedCell();
            tableView.SelectCellWithIdx(-1);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistEdierDomain _domain = new PlaylistEdierDomain();
        private Transform _playButtons;

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private static readonly SemaphoreSlim _createlistSemaphore = new SemaphoreSlim(1, 1);

        [UIComponent("playlists-list-table")]
        private CustomCellListTableData _playlists;

        
        private Button _playlistButton;

        [UIComponent("modal")]
        private ModalView _modal;
        [UIComponent("close-button")]
        private Button _closeButton;

        private PlaylistCellEntity.CellFactory _factory;
        [Inject]
        private DiContainer container;

        private LevelCollectionViewController levelCollectionViewController;
        private StandardLevelDetailViewController standardLevelDetailViewController;
        /// <summary>
        /// ボタンの位置によってモーダルウインドウの位置がずれるので開く前に強制的に座標を上書きさせる。
        /// </summary>
        private static readonly Vector3 _defaultLocalScale = new Vector3(0.8433125f, 1.5f, 2.6f);

        private const int LEVELID_LENGTH = 32;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
    }
}
