using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Notify;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberPlaylistsLib.Blist;
using HMUI;
using SimpleJSON;
using SongCore;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using UnityEngine;
using UnityEngine.UI;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;
using BeatSaberUI = SongPlayListEditer.BeatSaberCommon.BeatSaberUI;

namespace SongPlayListEditer.UI.Views
{
    internal class SimplePlayListView : SingletonBindableBase<SimplePlayListView>
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>
        /// For this method of setting the ResourceName, this class must be the first class in the file. 
        /// </summary>
        public string ResourceName => string.Join(".", GetType().Namespace, "SimplePlayListView.bsml");

        /// <summary>説明 を取得、設定</summary>
        private BeatSaberPlaylistsLib.Types.IPlaylist currentPlaylist_;
        /// <summary>説明 を取得、設定</summary>
        public BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist
        {
            get => this.currentPlaylist_;

            set => this.SetProperty(ref this.currentPlaylist_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private MainFlowCoordinator coordinater_;
        /// <summary>説明 を取得、設定</summary>
        public MainFlowCoordinator Coordinater
        {
            get => this.coordinater_;

            set => this.SetProperty(ref this.coordinater_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string addButtonText_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("add-text")]
        public string AddButtonText
        {
            get => this.addButtonText_;

            set => this.SetProperty(ref this.addButtonText_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private bool isButtonInteracive_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("playlist-button-interactive")]
        public bool IsButtonInteractive
        {
            get => this.isButtonInteracive_;

            set => this.SetProperty(ref this.isButtonInteracive_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private int currentCellIndex_;
        /// <summary>説明 を取得、設定</summary>
        public int CurrentCellIndex
        {
            get => this.currentCellIndex_;

            set => this.SetProperty(ref this.currentCellIndex_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private IPreviewBeatmapLevel beatmap_;
        /// <summary>説明 を取得、設定</summary>
        public IPreviewBeatmapLevel BeatMap
        {
            get => this.beatmap_;

            set => this.SetProperty(ref this.beatmap_, value);
        }

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void Awake()
        {
            base.Awake();
            Logger.Info("Start Awake");
            this.AddButtonText = "Add";
            _context = SynchronizationContext.Current;
            Logger.Info("Finish Awake");
        }
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

                this.IsButtonInteractive = false;
                if (!isAdded) {
                    this._playlists.tableView.SelectCellWithIdx(-1);
                }
                this._playlists.data.Clear();

                await Task.Run(() =>
                {
                    var beatmapHash = this.BeatMap.GetBeatmapHash().ToUpper();

                    foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                        if (playlist.Any(x => x.Hash?.ToUpper() == beatmapHash)) {
                            _context.Post(d =>
                            {
                                this._playlists.data.Add(new CustomCellInfo(playlist.Title, $"Song count-{playlist.Count}", Base64Sprites.StreamToTextuer2D(playlist.GetCoverStream()), new Sprite[1] { Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.sharp_playlist_add_check_white_18dp.png") }));
                            }, null);
                        }
                        else {
                            _context.Post(d =>
                            {
                                this._playlists.data.Add(new CustomCellInfo(playlist.Title, $"Song count-{playlist.Count}", Base64Sprites.StreamToTextuer2D(playlist.GetCoverStream())));
                            }, null);
                        }
                    }
                });

                Logger.Info($"Playlists count : {this._playlists.data.Count}");
                
                this._playlists.tableView.ReloadData();
                if (isAdded) {
                    this._playlists.tableView.SelectCellWithIdx(this.CurrentCellIndex);
                    this.SelectedPlaylist(null, this.CurrentCellIndex);
                }
                else {
                    this._playlists.tableView.SelectCellWithIdx(-1);
                }


                Logger.Info("Created List");
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                this.IsButtonInteractive = true;
                start.Stop();
                Logger.Info($"Creat time : {start.ElapsedMilliseconds}ms");
                _createlistSemaphore.Release();
            }
        }

        [UIAction("add-click")]
        public async Task AddClick()
        {
            try {
                await _semaphore.WaitAsync();

                var playlist = this.CurrentPlaylist;
                var addTarget = this.BeatMap;
                var addTargetHash = this.BeatMap.GetBeatmapHash().ToUpper();


                if (this.AddButtonText == "Add" && !playlist.Any(x => x.Hash?.ToUpper() == addTargetHash)) {
                    Logger.Info($"Start add song : {addTarget.songName}");
                    playlist.Add(addTargetHash, addTarget.songName, null, addTarget.levelAuthorName);
                    await this._domain.SavePlaylist(playlist);
                    Logger.Info($"Finish add song : {addTarget.songName}");
                }
                else {
                    Logger.Info($"Start delete song : {addTarget.songName}");
                    playlist.TryRemoveByHash(addTargetHash);
                    await this._domain.SavePlaylist(playlist);
                    Logger.Info($"Finish delete song : {addTarget.songName}");
                }
                await this.CreateList(true);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                _semaphore.Release();
                Logger.Info($"Finish add song");
            }
        }

        [UIAction("current")]
        public async void SelectedPlaylist(TableView table, int selectRow)
        {
            try {
                await _semaphore.WaitAsync();
                var beatMaphash = this.BeatMap.GetBeatmapHash().ToUpper();
                var result = await Task.Run(() => { return BeatSaberUtility.GetLocalPlaylist().ToArray(); });
                this.CurrentPlaylist = result[selectRow];
                this.CurrentCellIndex = selectRow;
                var isContain = this.CurrentPlaylist.Any(x => x.Hash?.ToUpper() == beatMaphash);
                Logger.Info($"Current PreviewBeatmapLevel LevelID : {this.BeatMap.levelID}");
                if (isContain) {
                    this.AddButtonText = "Delete";
                }
                else {
                    this.AddButtonText = "Add";
                }
            }
            catch (Exception e) {
                this.AddButtonText = "Add";
                Logger.Error(e);
            }
            finally {
                _semaphore.Release();
            }
        }

        internal void Setup()
        {
            standardLevel = Resources.FindObjectsOfTypeAll<StandardLevelDetailViewController>().First();            
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), BeatSaberUtility.PlayButtons.gameObject, this);
            _playlistButton = BeatSaberUI.CreateIconButton(BeatSaberUtility.PlayButtons, BeatSaberUtility.PracticeButton, Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.round_playlist_add_white_18dp.png"));
            _playlistButton.onClick.AddListener(this.ShowModal);
            BeatSaberUI.DestroyHoverHint(_playlistButton.transform as RectTransform);
            _playlistButton.GetComponentsInChildren<Image>().First(x => x.name == "Icon").transform.localScale *= 1.8f;
            _playlistButton.transform.SetSiblingIndex(0);
            BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent -= this.LevelCollectionViewController_didSelectLevelEvent;
            BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent += this.LevelCollectionViewController_didSelectLevelEvent;
        }

        private void LevelCollectionViewController_didSelectLevelEvent(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            Logger.Info($"Selected Beatmaplevel, [{arg2.songName} : {arg2.GetBeatmapHash()}]");

            this.BeatMap = arg2;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void ShowModal()
        {
            Logger.Info($"modal scale. [x : {this._modal.transform.position.x}, y : {this._modal.transform.position.y}, z : {this._modal.transform.position.z}]");
            this._modal.transform.position = _defaultLocalScale;
            this.CurrentPlaylist = null;
            this.CreateList();
            this._modal.Show(true);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistEdierDomain _domain = new PlaylistEdierDomain();

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private StandardLevelDetailViewController standardLevel;

        private static SynchronizationContext _context;

        private static readonly SemaphoreSlim _createlistSemaphore = new SemaphoreSlim(1, 1);

        [UIComponent("playlists")]
        private CustomListTableData _playlists;

        [UIComponent("playlist-button")]
        private Button _playlistButton;

        [UIComponent("modal")]
        private ModalView _modal;

        /// <summary>
        /// ボタンの位置によってモーダルウインドウの位置がずれるので開く前に強制的に座標を上書きさせる。
        /// </summary>
        private static readonly Vector3 _defaultLocalScale = new Vector3(0.8433125f, 1.64405f, 2.6f);
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
    }
}
