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
using HMUI;
using SimpleJSON;
using SongCore;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using UnityEngine;
using UnityEngine.UI;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace SongPlayListEditer.UI.Views
{
    internal class SimplePlayListView : NotifiableSingleton<SimplePlayListView>, INotifiableHost
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>
        /// For this method of setting the ResourceName, this class must be the first class in the file. 
        /// </summary>
        public string ResourceName => string.Join(".", GetType().Namespace, "SimplePlayListView.bsml");

        /// <summary>説明 を取得、設定</summary>
        private Playlist currentPlaylist_;
        /// <summary>説明 を取得、設定</summary>
        public Playlist CurrentPlaylist
        {
            get => this.currentPlaylist_;

            set => this.SetProperty(ref this.currentPlaylist_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private SimpleFlowCoordinater simpleFlowCoordinater_;
        /// <summary>説明 を取得、設定</summary>
        public SimpleFlowCoordinater SimpleFlowCoordinater
        {
            get => this.simpleFlowCoordinater_;

            set => this.SetProperty(ref this.simpleFlowCoordinater_, value);
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
        [UIAction("click-button")]
        public void CreateList()
        {
            _ = this.CreateList(false);
        }

        public async Task CreateList(bool isAdded)
        {
            var start = new Stopwatch();
            start.Start();
            try {
                Logger.Info("Create List");

                this.IsButtonInteractive = false;
                this._playlists.data.Clear();



                await Task.Run(() => {
                    var cellinfo = new List<CustomCellInfo>();
                    _context.Post(d => {
                        foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                            if (playlist.songs.Any(x => x.hash == BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash())) {
                                cellinfo.Add(new CustomCellInfo(playlist.playlistTitle, $"Song count-{playlist.songs.Count}", Base64Sprites.Base64ToTexture2D(playlist.image.Split(',').Last()), new Sprite[1] { Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.sharp_playlist_add_check_white_18dp.png") }));
                            }
                            else {
                                cellinfo.Add(new CustomCellInfo(playlist.playlistTitle, $"Song count-{playlist.songs.Count}", Base64Sprites.Base64ToTexture2D(playlist.image.Split(',').Last())));
                            }
                        }

                        this._playlists.data.AddRange(cellinfo);
                    }, null);
                });
                foreach (var cell in this._playlists.tableView.visibleCells) {
                    if (true) {
                        try {
                            cell.transform.Find("FavoritesIcon").gameObject.SetActive(true);
                            
                        }
                        catch (Exception e) {
                            Logger.Error(e);
                        }
                    }
                }

                Logger.Info($"Playlists count : {this._playlists.data.Count}");
                this._playlists.tableView.RefreshCells(true, true);
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
            }
        }

        [UIAction("add-click")]
        public async Task AddClick()
        {
            try {
                await _semaphore.WaitAsync();

                var addTarget = BeatSaberUtility.CurrentPreviewBeatmapLevel;

                if (this.AddButtonText == "Add" && !this.CurrentPlaylist.songs.Any(x => x.levelId == BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash())) {
                    Logger.Info($"Start add song : {addTarget.songName}");
                    this.CurrentPlaylist.songs.Add(new PlaylistSong()
                    {
                        key = "",
                        songName = addTarget.songName,
                        hash = addTarget.GetBeatmapHash(),
                    });
                    await this._domain.SavePlaylist(this.CurrentPlaylist);
                    Logger.Info($"Finish add song : {addTarget.songName}");
                }
                else {
                    Logger.Info($"Start delete song : {addTarget.songName}");
                    this.CurrentPlaylist.songs.RemoveAll(x => x.hash == addTarget.GetBeatmapHash());
                    await this._domain.SavePlaylist(this.CurrentPlaylist);
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
        public void SelectedPlaylist(TableView table, int selectRow)
        {
            var playlists = BeatSaberUtility.GetLocalPlaylist().ToArray();
            this.CurrentPlaylist = playlists[selectRow];
            this.CurrentCellIndex = selectRow;
            var isContain = this.CurrentPlaylist.songs.Any(x => x.hash == BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash());
            Logger.Info($"Current PreviewBeatmapLevel LevelID : {BeatSaberUtility.CurrentPreviewBeatmapLevel.levelID}");
            if (isContain) {
                this.AddButtonText = "Delete";
            }
            else {
                this.AddButtonText = "Add";
            }
        }

        internal void Setup()
        {
            standardLevel = Resources.FindObjectsOfTypeAll<StandardLevelDetailViewController>().First();
            BSMLParser.instance.Parse(Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), standardLevel.transform.Find("LevelDetail").gameObject, this);
            _playlistButtonTransform.localScale *= 0.4f;//no scale property in bsml as of now so manually scaling it
            _buttonIcon.sprite = Base64Sprites.LoadSpriteFromResources("SongPlayListEditer.Resources.round_playlist_add_white_18dp.png");
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void Awake()
        {
            Logger.Info("Start Awake");
            this.AddButtonText = "Add";
            _context = SynchronizationContext.Current;
            Logger.Info("Finish Awake");
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistEdierDomain _domain = new PlaylistEdierDomain();

        private readonly static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private StandardLevelDetailViewController standardLevel;

        private static SynchronizationContext _context;

        [UIComponent("playlists")]
        private CustomListTableData _playlists;

        [UIComponent("playlist-button")]
        private Transform _playlistButtonTransform;

        [UIComponent("button-icon")]
        private Image _buttonIcon;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region Prism
        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);
            NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="args">The PropertyChangedEventArgs</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            
        }
        #endregion
    }
}
