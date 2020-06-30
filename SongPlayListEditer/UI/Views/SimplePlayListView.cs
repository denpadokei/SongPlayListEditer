using System;
using System.Collections.Generic;
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
            try {
                Logger.Info("Create List");
                this.IsButtonInteractive = false;
                this._playlists.data.Clear();
                this._playlists.data
                    .AddRange(BeatSaberUtility.GetLocalPlaylist().Select(x => new CustomListTableData.CustomCellInfo(x.playlistTitle, $"Song count : {x.songs.Count}", Base64Sprites.Base64ToTexture2D(x.image))));
                Logger.Info($"Playlists count : {this._playlists.data.Count}");
                this._playlists.tableView.ReloadData();
                this._playlists.tableView.SelectCellWithIdx(-1);
                Logger.Info("Created List");
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                this.IsButtonInteractive = true;
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
                this.CreateList();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                //Loader.Instance.RefreshSongs();
                _semaphore.Release();
                Logger.Info($"Finish add song");
            }
        }

        [UIAction("current")]
        public void SelectedPlaylist(TableView table, int selectRow)
        {
            var playlists = BeatSaberUtility.GetLocalPlaylist().ToArray();
            this.CurrentPlaylist = playlists[selectRow];
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
            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), this.ResourceName), standardLevel.transform.Find("LevelDetail").gameObject, this);
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
            Logger.Info("Finish Awake");
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private PlaylistEdierDomain _domain = new PlaylistEdierDomain();

        private readonly static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private StandardLevelDetailViewController standardLevel;

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
