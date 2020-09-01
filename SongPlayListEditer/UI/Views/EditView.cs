using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Notify;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberPlaylistsLib;
using HMUI;
using PlaylistLoaderLite.HarmonyPatches;
using SongPlayListEditer.Bases;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using TMPro;
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
        private bool isSaveButtonIteractive_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("save-button-interactive")]
        public bool IsSaveButtonInteractive
        {
            get => this.isSaveButtonIteractive_;

            set => this.SetProperty(ref this.isSaveButtonIteractive_, value);
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
                this.CoverPath = null;
                this.Title = null;
                this.Author = null;
                this.Description = null;

                this.Title = this.Coordinator.CurrentPlaylist.Title;
                this.Author = this.Coordinator.CurrentPlaylist.Author;
                this.Description = this.Coordinator.CurrentPlaylist.Description;

                _cover.sprite = Base64Sprites.StreamToSprite(this.Coordinator.CurrentPlaylist?.GetCoverStream());

                this.IsSaveButtonInteractive = !string.IsNullOrEmpty(this.Title);
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(this.Title)) {
                this._parserParams?.EmitEvent("change-title");
                this.CanSave();
            }
            else if (args.PropertyName == nameof(this.Author)) {
                this._parserParams?.EmitEvent("change-author");
            }
            else if (args.PropertyName == nameof(this.Description)) {
                this._parserParams?.EmitEvent("change-description");
            }
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
        void Save()
        {
            Logger.Info("Clicked Save.");
            if (string.IsNullOrEmpty(this.Coordinator.CurrentPlaylist.Filename)) {
                this.Coordinator.CurrentPlaylist.Filename = this.Coordinator.CurrentPlaylist.Title;
            }
            Logger.Info($"Cover Path : [{this.CoverPath}]");
            Logger.Info($"Has Cover? : {this.Coordinator.CurrentPlaylist.HasCover}");
            if (!string.IsNullOrEmpty(this.CoverPath)) {
                using (var stream = new FileStream(this.CoverPath, FileMode.Open, FileAccess.Read)) {
                    this.Coordinator.CurrentPlaylist.SetCover(stream);
                }
            }
            else if(!this.Coordinator.CurrentPlaylist.HasCover) {
                using (var stream = Base64Sprites.Base64ToStream(DefaultImage.DEFAULT_IMAGE)) {
                    this.Coordinator.CurrentPlaylist.SetCover(stream);
                }
            }
            if (string.IsNullOrEmpty(this.Coordinator.CurrentPlaylist.Filename)) {
                this.Coordinator.CurrentPlaylist.Filename = $"{this.Title}_{DateTime.Now:yyyyMMddHHmmss}";
            }

            this.Coordinator.CurrentPlaylist.Title = this.Title;
            this.Coordinator.CurrentPlaylist.Author = this.Author;
            this.Coordinator.CurrentPlaylist.Description = this.Description;
            PlaylistManager.DefaultManager.StorePlaylist(this.Coordinator.CurrentPlaylist);

            try {
                PlaylistCollectionOverride.refreshPlaylists();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
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
            CreateCoverList();
        }

        [UIAction("open-folder")]
        void OpenCoverFolder()
        {
            Logger.Info("Clicked Open Folder.");
            Logger.Info($"Cover folder path : {PluginConfig.Instance.CoverDirectoryPath}");
            Process.Start($"{PluginConfig.Instance.CoverDirectoryPath}");
        }

        private void CreateCoverList()
        {
            this._covers.data.Clear();
            foreach (var coverPath in Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.jpg", SearchOption.TopDirectoryOnly).Union(Directory.EnumerateFiles(PluginConfig.Instance.CoverDirectoryPath, "*.png", SearchOption.TopDirectoryOnly))) {
                var fileinfo = new FileInfo(coverPath);
                this._covers.data.Add(new CustomCellInfo(fileinfo.Name, "", Base64Sprites.ImageFileToTextuer2D(coverPath)));
            }
            this._covers.tableView.ReloadData();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("covers")]
        CustomListTableData _covers;

        [UIComponent("cover")]
        Image _cover;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
