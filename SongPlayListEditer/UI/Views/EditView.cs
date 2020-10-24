﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
// using BeatSaberMarkupLanguage.Notify;
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
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try {
                base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
                this.CoverPath = null;
                this.Title = null;
                this.Author = null;
                this.Description = null;

                this.Title = this.CurrentPlaylist.Title;
                this.Author = this.CurrentPlaylist.Author;
                this.Description = this.CurrentPlaylist.Description;

                _cover.sprite = Base64Sprites.StreamToSprite(this.CurrentPlaylist?.GetCoverStream());

                this.IsSaveButtonInteractive = !string.IsNullOrEmpty(this.Title);
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
            if (string.IsNullOrEmpty(this.CurrentPlaylist.Filename)) {
                this.CurrentPlaylist.Filename = this.CurrentPlaylist.Title;
            }
            Logger.Info($"Cover Path : [{this.CoverPath}]");
            Logger.Info($"Has Cover? : {this.CurrentPlaylist.HasCover}");
            if (!string.IsNullOrEmpty(this.CoverPath)) {
                using (var stream = new FileStream(this.CoverPath, FileMode.Open, FileAccess.Read)) {
                    this.CurrentPlaylist.SetCover(stream);
                }
            }
            else if(!this.CurrentPlaylist.HasCover) {
                using (var stream = Base64Sprites.Base64ToStream(DefaultImage.DEFAULT_IMAGE)) {
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
            PlaylistCollectionOverride.RefreshPlaylists();
        }

        [UIAction("back")]
        void Back()
        {
            Logger.Info("Clicked Back");
            this.ExitEvent?.Invoke();
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
            _ = CreateCoverList();
        }

        [UIAction("open-folder")]
        void OpenCoverFolder()
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
        CustomListTableData _covers;

        [UIComponent("cover")]
        Image _cover;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
