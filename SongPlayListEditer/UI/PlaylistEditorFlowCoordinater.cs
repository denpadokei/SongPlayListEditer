﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SongPlayListEditer.Models;
using SongPlayListEditer.UI.Views;
using SongPlayListEditer.Utilites;
using System;
using Zenject;

namespace SongPlayListEditer.UI
{
    public class PlaylistEditorFlowCoordinator : FlowCoordinator
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ

        /// <summary>説明 を取得、設定</summary>
        public BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist { get; private set; }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation) {
                this.SetTitle("PLAYLIST EDITOR");
                this.showBackButton = true;
                this.ProvideInitialViewControllers(this._playListMenuView);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            base.BackButtonWasPressed(topViewController);
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void ShowPlaylist()
        {
            Logger.Info("ShowPlaylistView");
            this.ReplaceTopViewController(this._playListMenuView, null, ViewController.AnimationType.In, ViewController.AnimationDirection.Vertical);
        }

        public void ShowEdit()
        {
            Logger.Info("ShowEditView");
            this._editView.CurrentPlaylist = this.CurrentPlaylist;
            this.ReplaceTopViewController(this._editView, null, ViewController.AnimationType.Out, ViewController.AnimationDirection.Vertical);
        }

        public void ShowAdd()
        {
            this.SetCurrentPlaylist(PlaylistLibUtility.CurrentManager.CreatePlaylist(null, "", "", ""));
            this.ShowEdit();
        }

        public void SetCurrentPlaylist(BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {

            this.CurrentPlaylist = playlist;
            if (playlist == null) {
                this._editView.Title = "";
                this._editView.Author = "";
                this._editView.Description = "";
                return;
            }
            else {
                this._editView.Title = playlist.Title;
                this._editView.Author = playlist.Author;
                this._editView.Description = playlist.Description;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void Start()
        {
            try {
                Logger.Info($"AwakeStart");
                Logger.Info($"Is playlist null? {this._playListMenuView == null}");
                Logger.Info($"Is edit null? {this._editView == null}");
                this._playListMenuView.ShowAddEvent -= this.ShowAdd;
                this._playListMenuView.ShowEditEvent -= this.ShowEdit;
                this._playListMenuView.SelectedCell -= this.SetCurrentPlaylist;
                this._editView.SaveEvent -= this.Save;
                this._editView.ExitEvent -= this.ShowPlaylist;


                this._playListMenuView.ShowAddEvent += this.ShowAdd;
                this._playListMenuView.ShowEditEvent += this.ShowEdit;
                this._playListMenuView.SelectedCell += this.SetCurrentPlaylist;
                this._editView.SaveEvent += this.Save;
                this._editView.ExitEvent += this.ShowPlaylist;
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private void OnDestroy()
        {
            this._playListMenuView.ShowAddEvent -= this.ShowAdd;
            this._playListMenuView.ShowEditEvent -= this.ShowEdit;
            this._playListMenuView.SelectedCell -= this.SetCurrentPlaylist;
            this._editView.SaveEvent -= this.Save;
            this._editView.ExitEvent -= this.ShowPlaylist;
            MenuButtons.instance.UnregisterButton(this.menuButton);
        }

        private void Save(BeatSaberPlaylistsLib.Types.IPlaylist playlists)
        {
            if (playlists == null) {
                return;
            }
            PlaylistLibUtility.CurrentManager.StorePlaylist(playlists);
        }

        /// <summary>
        /// プレイリスト編集画面に遷移します。
        /// </summary>
        private void ShowMainFlowCoodniator() => BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [Inject]
        private PlayListMenuView _playListMenuView;
        [Inject]
        private EditView _editView;
        private MenuButton menuButton;

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        private void Constractor()
        {
            this.menuButton = new MenuButton("SONG PLAYLIST EDITER", "Edit song playlist", this.ShowMainFlowCoodniator);
            MenuButtons.instance.RegisterButton(this.menuButton);
        }
        #endregion
    }
}
