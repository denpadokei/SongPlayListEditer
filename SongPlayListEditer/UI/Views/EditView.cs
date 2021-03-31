using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using SongPlayListEditer.Bases;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using SongPlayListEditer.Utilites;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine.UI;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace SongPlayListEditer.UI.Views
{
    [HotReload]
    public class EditView : ViewContlollerBindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        // For this method of setting the ResourceName, this class must be the first class in the file.
        /// <summary>プレイリストのタイトル を取得、設定</summary>
        private string title_;
        /// <summary>プレイリストのタイトル を取得、設定</summary>
        [UIValue("title")]
        public string Title
        {
            get => this.title_ ?? "";

            set => this.SetProperty(ref this.title_, value);
        }

        /// <summary>作者 を取得、設定</summary>
        private string author_;
        /// <summary>作者 を取得、設定</summary>
        [UIValue("author")]
        public string Author
        {
            get => this.author_ ?? "";

            set => this.SetProperty(ref this.author_, value);
        }

        /// <summary>詳細 を取得、設定</summary>
        private string description_;
        /// <summary>詳細 を取得、設定</summary>
        [UIValue("description")]
        public string Description
        {
            get => this.description_ ?? "";

            set => this.SetProperty(ref this.description_, value);
        }

        /// <summary>ロックしてるかどうか を取得、設定</summary>
        private bool isLocked_;
        [UIValue("is-locked")]
        /// <summary>ロックしてるかどうか を取得、設定</summary>
        public bool IsLocked
        {
            get => this.isLocked_;

            set => this.SetProperty(ref this.isLocked_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string coverPath_;
        /// <summary>説明 を取得、設定</summary>
        public string CoverPath
        {
            get => this.coverPath_ ?? "";

            set => this.SetProperty(ref this.coverPath_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private bool isSaveButtonIteractive_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("save-button-interactive")]
        public bool IsSaveButtonInteractive
        {
            get => this.isSaveButtonIteractive_;

            set => this.SetProperty(ref this.isSaveButtonIteractive_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private BeatSaberPlaylistsLib.Types.IPlaylist curretPlaylist_;
        /// <summary>説明 を取得、設定</summary>
        public BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist
        {
            get => this.curretPlaylist_;

            set => this.SetProperty(ref this.curretPlaylist_, value);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        public event Action<BeatSaberPlaylistsLib.Types.IPlaylist> SaveEvent;
        public event Action ExitEvent;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // Unity massage
        private void Awake() => this.lockedPlaylistEntity = new LockedPlaylistEntity();
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try {
                base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
                this.lockedPlaylistEntity = this.lockedPlaylistEntity.Read();
                this.CoverPath = null;
                this.Title = null;
                this.Author = null;
                this.Description = null;

                this.Title = this.CurrentPlaylist.Title;
                this.Author = this.CurrentPlaylist.Author;
                this.Description = this.CurrentPlaylist.Description;
                this.IsLocked = this.lockedPlaylistEntity.LockedPlaylists.Any(x => Regex.IsMatch(x, $"^{this.CurrentPlaylist.Filename}$", RegexOptions.IgnoreCase));

                this._cover.sprite = Base64Sprites.StreamToSprite(this.CurrentPlaylist?.GetCoverStream());

                this.IsSaveButtonInteractive = !string.IsNullOrEmpty(this.Title);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            this.CanSave();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void CanSave()
        {
            if (string.IsNullOrEmpty(this.Title)) {
                this.IsSaveButtonInteractive = false;
            }
            else {
                this.IsSaveButtonInteractive = true;
            }
        }

        [UIAction("save")]
        private void Save()
        {
            Logger.Info("Clicked Save.");
            if (string.IsNullOrEmpty(this.CurrentPlaylist.Filename)) {
                this.CurrentPlaylist.Filename = this.CurrentPlaylist.Title;
            }
            Logger.Info($"Cover Path : [{this.CoverPath}]");
            Logger.Info($"Has Cover? : {this.CurrentPlaylist.HasCover}");
            if (!string.IsNullOrEmpty(this.CoverPath)) {
                using (var stream = new FileStream(this.CoverPath, FileMode.Open, FileAccess.Read)) {
                    var tex = Base64Sprites.StreamToTextuer2D(stream);
                    Base64Sprites.CashedTextuer.AddOrUpdate(this.CurrentPlaylist.Filename, tex, (s, t) => tex);
                    stream.Position = 0;
                    this.CurrentPlaylist.SetCover(stream);
                }
            }
            else if (!this.CurrentPlaylist.HasCover) {
                using (var stream = Base64Sprites.Base64ToStream(DefaultImage.DEFAULT_IMAGE)) {
                    var tex = Base64Sprites.StreamToTextuer2D(stream);
                    Base64Sprites.CashedTextuer.AddOrUpdate(this.CurrentPlaylist.Filename, tex, (s, t) => tex);
                    stream.Position = 0;
                    this.CurrentPlaylist.SetCover(stream);
                }
            }
            if (string.IsNullOrEmpty(this.CurrentPlaylist.Filename)) {
                this.CurrentPlaylist.Filename = $"{this.Title}_{DateTime.Now:yyyyMMddHHmmss}";
            }

            this.CurrentPlaylist.Title = this.Title;
            this.CurrentPlaylist.Author = this.Author;
            this.CurrentPlaylist.Description = this.Description;
            this.SaveEvent?.Invoke(this.CurrentPlaylist);
            if (this.IsLocked) {
                this.lockedPlaylistEntity = this.lockedPlaylistEntity.Add(this.CurrentPlaylist.Filename);
            }
            else {
                this.lockedPlaylistEntity = this.lockedPlaylistEntity.Remove(this.CurrentPlaylist.Filename);
            }
            this.lockedPlaylistEntity.Save();
            PlaylistLibUtility.RefreshPlaylists();
        }

        [UIAction("back")]
        private void Back()
        {
            Logger.Info("Clicked Back");
            this.ExitEvent?.Invoke();
        }

        [UIAction("select-cover-cell")]
        private void SelectCoverCell(TableView tableView, int cellindex)
        {
            var cell = this._covers.data[cellindex];
            var files = Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).Union(Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.png", SearchOption.TopDirectoryOnly)).Select(x => new FileInfo(x));
            try {
                var fileinfo = files.FirstOrDefault(x => x.Name == cell.text);
                this.CoverPath = fileinfo.FullName;
                using (var stream = new FileStream(fileinfo.FullName, FileMode.Open, FileAccess.Read)) {
                    this._cover.sprite = Base64Sprites.StreamToSprite(stream);
                }
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        [UIAction("select-cover")]
        private void ShowModal()
        {
            Logger.Info("Clicked Show modal");
            _ = this.CreateCoverList();
        }

        [UIAction("open-folder")]
        private void OpenCoverFolder()
        {
            Logger.Info("Clicked Open Folder.");
            Logger.Info($"Cover folder path : {PluginConfig.Instance.CoverDirectoryPath}");
            Process.Start($"{PluginConfig.Instance.CoverDirectoryPath}");
        }

        private async Task CreateCoverList()
        {
            this._covers.data.Clear();

            await Task.Run(() =>
            {
                foreach (var coverPath in Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).Union(Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.png", SearchOption.TopDirectoryOnly))) {
                    var fileinfo = new FileInfo(coverPath);
                    HMMainThreadDispatcher.instance?.Enqueue(() =>
                    {
                        this._covers.data.Add(new CustomCellInfo(fileinfo.Name, "", Base64Sprites.ImageFileToSprite(coverPath)));
                    });
                }
                HMMainThreadDispatcher.instance?.Enqueue(() =>
                {
                    this._covers.tableView.ReloadData();
                });
            });
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("covers")]
        private readonly CustomListTableData _covers;
        [UIComponent("cover")]
        private readonly Image _cover;
        private LockedPlaylistEntity lockedPlaylistEntity;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
