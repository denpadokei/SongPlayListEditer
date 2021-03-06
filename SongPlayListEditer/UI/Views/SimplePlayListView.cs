﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BS_Utils.Utilities;
using HMUI;
using IPA.Loader;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Interfaces;
using SongPlayListEditer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using PlaylistUI = SongPlayListEditer.BeatSaberCommon.PlaylistUI;

namespace SongPlayListEditer.UI.Views
{
    [HotReload]
    public class SimplePlayListView : ViewContlollerBindableBase, IInitializable
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>
        /// For this method of setting the ResourceName, this class must be the first class in the file. 
        /// </summary>
        public string ResourceName => string.Join(".", this.GetType().Namespace, "SimplePlayListView.bsml");
        [UIValue("playlists")]
        private List<object> Playlists { get; } = new List<object>();
        public static bool IsInstalledSongBrowser { get; private set; }

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
        public async Task CreateList()
        {
            await _createlistSemaphore.WaitAsync();

            var start = new Stopwatch();
            start.Start();
            try {
                Logger.Info("Create List");
                this.Playlists.Clear();
                this._playlists.tableView.ReloadData();

                foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                    if (this.LockedPlaylistEntity.LockedPlaylists.Any(x => Regex.IsMatch(x, $"^{playlist.Filename}$", RegexOptions.IgnoreCase))) {
                        continue;
                    }
                    var cell = this._factory.Create(playlist, this.Beatmap);
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

            this._modal?.Hide(true);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        [Inject]
        private void Constractor(PlaylistCellEntity.CellFactory cellFactory) => this._factory = cellFactory;

        public void Initialize()
        {
            try {
                IsInstalledSongBrowser = PluginManager.GetPlugin("Song Browser") != null;
                this.standardLevelDetailViewController = this.container.Resolve<StandardLevelDetailViewController>();
                this.levelCollectionViewController = this.container.Resolve<LevelCollectionViewController>();
                var standardLevelDetailView = this.standardLevelDetailViewController.GetPrivateField<StandardLevelDetailView>("_standardLevelDetailView");
                Logger.Debug($"standardLevelDetailView is null? : {standardLevelDetailView == null}");
                var playContainer = standardLevelDetailView.GetComponentsInChildren<RectTransform>().First(x => x.name == "BeatmapParamsPanel");
                this._playButtons = standardLevelDetailView.GetComponentsInChildren<RectTransform>().First(x => x.name == "ActionButtons");
                BSMLParser.instance?.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), this.rectTransform.gameObject, this);
                var button = Resources.FindObjectsOfTypeAll<Button>().First(x => x.name == "PracticeButton").transform as RectTransform;

#if DEBUG
                this._playlistButton = PlaylistUI.CreateIconButton("PlaylistButton", this._playButtons.transform as RectTransform, Vector2.zero, new Vector2(6f, button.sizeDelta.y), this.ShowModal, Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.predator.png"), "PracticeButton");
                this._playlistButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 0.9f;
                this._playlistButton.gameObject.GetComponentsInChildren<LayoutElement>(true).FirstOrDefault().preferredWidth = 11f;
#else
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1) {
                    this._playlistButton = PlaylistUI.CreateIconButton("PlaylistButton", this._playButtons.transform as RectTransform, Vector2.zero, new Vector2(6f, button.sizeDelta.y), this.ShowModal, Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.predator.png"), "PracticeButton");
                    this._playlistButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 0.9f;
                    this._playlistButton.gameObject.GetComponentsInChildren<LayoutElement>(true).FirstOrDefault().preferredWidth = 11f;
                }
                else {
                    this._playlistButton = PlaylistUI.CreateIconButton("PlaylistButton", this._playButtons.transform as RectTransform, Vector2.zero, new Vector2(6f, button.sizeDelta.y), this.ShowModal, Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.round_playlist_add_white_18dp.png"), "PracticeButton");
                    this._playlistButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 2.1f;
                }
#endif

                Logger.Debug($"Button is null? : {this._playlistButton == null}");
                Logger.Debug($"modal is null? : {this._modal == null}");
                this._playlistButton.transform.SetSiblingIndex(PluginConfig.Instance.ButtonIndex);
                this._playlistButton.interactable = true;
                this.levelCollectionViewController.didSelectLevelEvent -= this.LevelCollectionViewController_didSelectLevelEvent;
                this.levelCollectionViewController.didSelectLevelEvent += this.LevelCollectionViewController_didSelectLevelEvent;
                PlaylistUI.ConvertIconButton(ref this._closeButton, new Vector2(50f, 50f), Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.baseline_close_white_18dp.png"));
                this._closeButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 1.2f;
                if (IsInstalledSongBrowser) {
                    HMMainThreadDispatcher.instance.Enqueue(this.ResizeDeleteButton());
                }
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private IEnumerator ResizeDeleteButton()
        {
            Logger.Debug("Start Resize DeleteButton");
            yield return new WaitWhile(() => this._playButtons.GetComponentsInChildren<Button>(true).FirstOrDefault(x => x.name.Contains("DeleteLevelButton")) == null);
            var deletebutton = this._playButtons.GetComponentsInChildren<Button>().FirstOrDefault(x => x.name.Contains("DeleteLevelButton"));
            deletebutton.gameObject.GetComponentsInChildren<LayoutElement>(true).FirstOrDefault().preferredWidth = 12f;
            Logger.Debug("Finish Resize DeleteButton");
        }

        private void ShowModal()
        {
            Logger.Info($"modal scale. [x : {this._modal.transform.position.x}, y : {this._modal.transform.position.y}, z : {this._modal.transform.position.z}]");

            this.LockedPlaylistEntity = this.LockedPlaylistEntity.Read();
            this._modal.transform.SetParent(this.standardLevelDetailViewController.rectTransform, false);
            this._modal.transform.position = _defaultLocalScale;
            this._modal.Show(true, false, () => { _ = this.CreateList(); });
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
        private Transform _playButtons;

        private static readonly SemaphoreSlim _createlistSemaphore = new SemaphoreSlim(1, 1);

        [UIComponent("playlists-list-table")]
        private readonly CustomCellListTableData _playlists;


        private Button _playlistButton;

        [UIComponent("modal")]
        private readonly ModalView _modal;
        [UIComponent("close-button")]
        private Button _closeButton;

        private PlaylistCellEntity.CellFactory _factory;
        [Inject]
        private readonly DiContainer container;

        private LevelCollectionViewController levelCollectionViewController;
        private StandardLevelDetailViewController standardLevelDetailViewController;
        private LockedPlaylistEntity LockedPlaylistEntity = new LockedPlaylistEntity();
        /// <summary>
        /// ボタンの位置によってモーダルウインドウの位置がずれるので開く前に強制的に座標を上書きさせる。
        /// x : 1.221752, y : 1.052058, z : 4.02051
        /// </summary>
        private static readonly Vector3 _defaultLocalScale = new Vector3(1.221752f, 1.41f, 4.02051f);

        //private const int LEVELID_LENGTH = 32;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
