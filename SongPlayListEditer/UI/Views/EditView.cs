using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberPlaylistsLib;
using HMUI;
using Newtonsoft.Json;
using SongPlayListEditer.Bases;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using UnityEngine;
using UnityEngine.UI;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace SongPlayListEditer.UI.Views
{
    public class EditView : ViewContlollerBindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => string.Join(".", GetType().Namespace, "EditView.bsml");

        /// <summary>プレイリストのタイトル を取得、設定</summary>
        private string title_;
        /// <summary>プレイリストのタイトル を取得、設定</summary>
        [UIValue("title")]
        public string Title
        {
            get => this.title_;

            set => this.SetProperty(ref this.title_, value);
        }

        /// <summary>作者 を取得、設定</summary>
        private string author_;
        /// <summary>作者 を取得、設定</summary>
        [UIValue("author")]
        public string Author
        {
            get => this.author_;

            set => this.SetProperty(ref this.author_, value);
        }

        /// <summary>詳細 を取得、設定</summary>
        private string description_;
        /// <summary>詳細 を取得、設定</summary>
        [UIValue("description")]
        public string Description
        {
            get => this.description_;

            set => this.SetProperty(ref this.description_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string coverPath_;
        /// <summary>説明 を取得、設定</summary>
        public string CoverPath
        {
            get => this.coverPath_;

            set => this.SetProperty(ref this.coverPath_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private MainFlowCoordinator coordinater_;
        /// <summary>説明 を取得、設定</summary>
        public MainFlowCoordinator Coordinator
        {
            get => this.coordinater_;

            set => this.SetProperty(ref this.coordinater_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private int currentCover_;
        /// <summary>説明 を取得、設定</summary>
        public int CurrentCover
        {
            get => this.currentCover_;

            set => this.SetProperty(ref this.currentCover_, value);
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
        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            try {
                base.DidActivate(firstActivation, type);
                this.Title = "";
                this.Author = "";
                this.Description = "";

                this.Title = this.Coordinator.CurrentPlaylist.Title;
                this.Author = this.Coordinator.CurrentPlaylist.Author;
                this.Description = this.Coordinator.CurrentPlaylist.Description;

                _cover.sprite = Base64Sprites.StreamToSprite(this.Coordinator.CurrentPlaylist?.GetCoverStream());
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        protected override void Awake()
        {
            base.Awake();
            _context = SynchronizationContext.Current;
        }

        [UIAction("save")]
        void Save()
        {
            Logger.Info("Clicked Save.");
            if (string.IsNullOrEmpty(this.Coordinator.CurrentPlaylist.Filename)) {
                this.Coordinator.CurrentPlaylist.Filename = this.Coordinator.CurrentPlaylist.Title;
            }
            using (var stream = new FileStream(this.CoverPath, FileMode.Open, FileAccess.Read)) {
                this.Coordinator.CurrentPlaylist.SetCover(stream);
            }
            this.Coordinator.CurrentPlaylist.Title = this.Title;
            this.Coordinator.CurrentPlaylist.Author = this.Author;
            this.Coordinator.CurrentPlaylist.Description = this.Description;
            File.WriteAllText(Path.Combine(FilePathName.PlaylistsFolderPath, $"{this.Coordinator.CurrentPlaylist?.Filename}.json"), JsonConvert.SerializeObject(this.Coordinator.CurrentPlaylist, Formatting.Indented));
            
        }

        [UIAction("back")]
        void Back()
        {
            Logger.Info("Clicked Back");
            this.Coordinator.ShowPlaylist();
        }

        [UIAction("select-cover-cell")]
        void SelectCoverCell(TableView tableView, int cellindex)
        {
            var cell = this._covers.data[cellindex];
            var files = Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).Union(Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.png", SearchOption.TopDirectoryOnly)).Select(x => new FileInfo(x));
            try {
                var fileinfo = new FileInfo(files.FirstOrDefault(x => x.Name == cell.text).FullName);
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
            _ = CreateCoverList();
        }

        private async Task CreateCoverList()
        {
            this._covers.data.Clear();
            await Task.Run(() =>
            {
                foreach (var coverPath in Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).Union(Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.png", SearchOption.TopDirectoryOnly))) {
                    var fileinfo = new FileInfo(coverPath);
                    _context.Post(d =>
                    {
                        this._covers.data.Add(new CustomCellInfo(fileinfo.Name, "", Base64Sprites.ImageFileToTextuer2D(coverPath)));
                    }, null);
                }
            });

            this._covers.tableView.ReloadData();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("covers")]
        CustomListTableData _covers;

        [UIComponent("cover")]
        Image _cover;

        static SynchronizationContext _context;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
