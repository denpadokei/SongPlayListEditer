#define BeatSaber
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Notify;
using HMUI;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.DataBases;
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
        [UIValue("add-button-interactive")]
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

        /// <summary>説明 を取得、設定</summary>
        private SongTypeMode songType;
        /// <summary>説明 を取得、設定</summary>
        public SongTypeMode SongType
        {
            get => this.songType;

            set => this.SetProperty(ref this.songType, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private AddMode add_;
        /// <summary>説明 を取得、設定</summary>
        public AddMode Add
        {
            get => this.add_;

            set => this.SetProperty(ref this.add_, value);
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
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.CurrentPlaylist)) {
                this.IsButtonInteractive = this.CurrentPlaylist != null;
            }
        }

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
                    var beatmapLevelID = this.BeatMap.levelID.ToUpper();

                    foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                        var isContain = false;

                        switch (this.SongType) {
                            case SongTypeMode.Custom:
                                isContain = playlist.Any(x => x.Hash?.ToUpper() == beatmapHash);
                                break;
                            case SongTypeMode.Official:
                                isContain = playlist.Any(x => x.LevelId?.ToUpper() == beatmapLevelID);
                                break;
                            default:
                                break;
                        }

                        var cover = playlist.GetCoverStream();
                        if (isContain) {
                            _context.Post(d =>
                            {
                                this._playlists.data.Add(new CustomCellInfo(playlist.Title, $"Song count-{playlist.Count}", Base64Sprites.StreamToTextuer2D(cover), new Sprite[1] { Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.sharp_playlist_add_check_white_18dp.png") }));
                            }, null);
                        }
                        else {
                            _context.Post(d =>
                            {
                                this._playlists.data.Add(new CustomCellInfo(playlist.Title, $"Song count-{playlist.Count}", Base64Sprites.StreamToTextuer2D(cover)));
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
                var addTargetLevelID = this.BeatMap.levelID.ToUpper();

                var isContain = false;

                switch (this.SongType) {
                    case SongTypeMode.Custom:
                        isContain = playlist.Any(x => x.Hash?.ToUpper() == addTargetHash);
                        break;
                    case SongTypeMode.Official:
                        isContain = playlist.Any(x => x.LevelId?.ToUpper() == addTargetLevelID);
                        break;
                    default:
                        break;
                }

                Logger.Info($"Is Contain? : {isContain}");
                Logger.Info($"Mode? : {this.Add}");

                if (this.Add == AddMode.Add && !isContain) {
                    Logger.Info($"Start add song : {addTarget.songName}");
                    playlist.AllowDuplicates = true;
                    await this.AddSong(addTarget, playlist);
                    await this._domain.SavePlaylist(playlist);
                    Logger.Info($"Finish add song : {addTarget.songName}");
                }
                else {
                    Logger.Info($"Start delete song : {addTarget.songName}");
                    this.RemoveSong(addTarget, playlist);
                    await this._domain.SavePlaylist(playlist);
                    Logger.Info($"Finish delete song : {addTarget.songName}");
                }
                await this.CreateList(true);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                this.RaisePropertyChanged(nameof(this.CurrentPlaylist));
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
                var beatMapLevelID = this.BeatMap.levelID.ToUpper();
                var result = await Task.Run(() => { return BeatSaberUtility.GetLocalPlaylist().ToArray(); });
                this.CurrentPlaylist = result[selectRow];
                this.CurrentCellIndex = selectRow;
                var isContain = false;

                switch (this.SongType) {
                    case SongTypeMode.Custom:
                        isContain = this.CurrentPlaylist.Any(x => x.Hash?.ToUpper() == beatMaphash);
                        break;
                    case SongTypeMode.Official:
                        isContain = this.CurrentPlaylist.Any(x => x.LevelId?.ToUpper() == beatMapLevelID);
                        break;
                    default:
                        break;
                }

                Logger.Info($"Current PreviewBeatmapLevel LevelID : {this.BeatMap.levelID}");
                if (isContain) {
                    this.AddButtonText = "Delete";
                    this.Add = AddMode.Remove;
                }
                else {
                    this.AddButtonText = "Add";
                    this.Add = AddMode.Add;
                }
            }
            catch (Exception e) {
                this.CurrentPlaylist = null;
                this.AddButtonText = "Add";
                this.Add = AddMode.Add;
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
            _playlistButton.transform.SetSiblingIndex(PluginConfig.Instance.ButtonIndex);
            BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent -= this.LevelCollectionViewController_didSelectLevelEvent;
            BeatSaberUtility.LevelCollectionViewController.didSelectLevelEvent += this.LevelCollectionViewController_didSelectLevelEvent;
        }

        private void LevelCollectionViewController_didSelectLevelEvent(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            Logger.Info($"Selected Beatmaplevel, [{arg2.songName} : {arg2.GetBeatmapHash()}]");

            this.BeatMap = arg2;

            if (arg2.IsCustom()) {
                this._playlistButton.interactable = true;
                this.SongType = SongTypeMode.Custom;
            }
            else if (arg2.IsWip()) {
                this._playlistButton.interactable = false;
                this.SongType = SongTypeMode.WIP;
            }
            else if (arg2.IsOfficial()) {
                this._playlistButton.interactable = true;
                this.SongType = SongTypeMode.Official;
            }
            else {
                this._playlistButton.interactable = false;
                this.SongType = SongTypeMode.None;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void ShowModal()
        {
            Logger.Info($"modal scale. [x : {this._modal.transform.position.x}, y : {this._modal.transform.position.y}, z : {this._modal.transform.position.z}]");
            this._modal.transform.position = _defaultLocalScale;
            this.CurrentPlaylist = null;
            this.AddButtonText = "Add";
            this.CreateList();
            this._modal.Show(true);
        }

        private async Task AddSong(IPreviewBeatmapLevel beatmap, BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            switch (this.SongType) {
                case SongTypeMode.Custom:
                    var addTargetHash = this.BeatMap.GetBeatmapHash();
                    if (PluginConfig.Instance.IsSaveWithKey) {
                        playlist.Add(addTargetHash, beatmap.songName, await BeatSarverData.GetBeatMapKey(addTargetHash), beatmap.levelAuthorName);
                    }
                    else {
                        playlist.Add(addTargetHash, beatmap.songName, null, beatmap.levelAuthorName);
                    }
                    break;
                case SongTypeMode.Official:
                    var officalSong = new OfficialSongEntity()
                    {
                        LevelId = beatmap.levelID,
                        Name = beatmap.songName,
                        Identifiers = BeatSaberPlaylistsLib.Types.Identifier.LevelId,
                        DateAdded = DateTime.Now
                    };

                    playlist.Add(officalSong);
                    break;
                default:
                    break;
            }
        }

        private void RemoveSong(IPreviewBeatmapLevel beatmap, BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            switch (this.SongType) {
                case SongTypeMode.Custom:
                    playlist.TryRemoveByHash(beatmap.GetBeatmapHash());
                    break;
                case SongTypeMode.Official:
                    var levelID = beatmap.levelID.ToUpper();
                    playlist.Remove(playlist.FirstOrDefault(x => x.LevelId?.ToUpper() == levelID));
                    break;
                default:
                    break;
            }
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

        private const int LEVELID_LENGTH = 32;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
    }
}
