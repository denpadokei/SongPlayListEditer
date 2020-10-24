using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberPlaylistsLib;
using HMUI;
using IPA.Utilities;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.DataBases;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Interfaces;
using SongPlayListEditer.Statics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace SongPlayListEditer.Models
{
    public class PlaylistCellEntity : BindableBase, IPlaylistCell
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        /// <summary>説明 を取得、設定</summary>
        private BeatSaberPlaylistsLib.Types.IPlaylist currentPlaylist_;
        /// <summary>説明 を取得、設定</summary>
        public BeatSaberPlaylistsLib.Types.IPlaylist CurrentPlaylist
        {
            get => this.currentPlaylist_;

            set => this.SetProperty(ref this.currentPlaylist_, value);
        }

        /// <summary>説明 を取得、設定</summary>
        private string title_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("title")]
        public string Title
        {
            get => this.title_ ?? "";

            set => this.SetProperty(ref this.title_, value);
        }

        
        /// <summary>説明 を取得、設定</summary>
        private string subInfo_;
        /// <summary>説明 を取得、設定</summary>
        [UIValue("sub-info")]
        public string SubInfo
        {
            get => this.subInfo_ ?? "";

            set => this.SetProperty(ref this.subInfo_, value);
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
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        [UIAction("#post-parse")]
        public void PostParse()
        {
            HMMainThreadDispatcher.instance?.Enqueue(() =>
            {
                                this.Title = this.CurrentPlaylist.Title;
                this.SubInfo = $"Song count - {this.CurrentPlaylist.Count}";
                this._toggle = (this._checkBox as CurvedTextMeshPro).GetComponentsInParent<ToggleSetting>(true).First();
                this._toggle.Text = "";
                switch (this.SongType) {
                    case SongTypeMode.Official:
                        _toggle.toggle.isOn = this.CurrentPlaylist.Any(x => !string.IsNullOrEmpty(x.LevelId) && x.LevelId == this.BeatMap?.levelID);
                        break;
                    default:
                        _toggle.toggle.isOn = this.CurrentPlaylist.Any(x => x.Hash?.ToUpper() == this.BeatMap?.GetBeatmapHash().ToUpper());
                        break;
                }
                this._toggle.toggle.onValueChanged.AddListener(this.OnChange);
            });
            HMMainThreadDispatcher.instance?.Enqueue(this.SetCover());
            
        }

        public void SetPlaylistInformation(BeatSaberPlaylistsLib.Types.IPlaylist playlists, IPreviewBeatmapLevel beatmapLevel)
        {
            this.BeatMap = beatmapLevel;
            this.CurrentPlaylist = playlists;
            if (this.BeatMap?.IsCustom() == true) {
                this.SongType = SongTypeMode.Custom;
            }
            else if (this.BeatMap?.IsWip() == true) {
                this.SongType = SongTypeMode.WIP;
            }
            else if (this.BeatMap?.IsOfficial() == true) {
                this.SongType = SongTypeMode.Official;
            }
            else {
                this.SongType = SongTypeMode.None;
            }
        }

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド

        private async Task AddSong(IPreviewBeatmapLevel beatmap, BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            if (beatmap == null || playlist == null) {
                Logger.Debug($"beatmap is null? : {beatmap == null}");
                Logger.Debug($"playlist is null? : {playlist == null}");
                return;
            }


            switch (this.SongType) {
                case SongTypeMode.Custom:
                    var addTargetHash = this.BeatMap?.GetBeatmapHash();
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
            if (beatmap == null || playlist == null) {
                return;
            }
            switch (this.SongType) {
                case SongTypeMode.Custom:
                    playlist?.TryRemoveByHash(beatmap?.GetBeatmapHash());
                    break;
                case SongTypeMode.Official:
                    var levelID = beatmap.levelID.ToUpper();
                    playlist?.Remove(playlist?.FirstOrDefault(x => x.LevelId?.ToUpper() == levelID));
                    break;
                default:
                    break;
            }
        }
        private async void OnChange(bool value)
        {
            Logger.Debug($"change check box");
            Logger.Debug($"value : {value}");
            if (value) {
                await this.AddSong(BeatMap, CurrentPlaylist);
            }
            else {
                this.RemoveSong(BeatMap, CurrentPlaylist);

            }
            await this.SavePlaylist(CurrentPlaylist);

            this.SubInfo = $"Song count - {this.CurrentPlaylist.Count}";
        }

        private IEnumerator SetCover()
        {
            var stream = this.CurrentPlaylist.GetCoverStream();
            yield return null;
            var tex = Base64Sprites.StreamToTextuer2D(stream);
            yield return null;
            _cover.sprite = Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f));
        }

        private async Task SavePlaylist(BeatSaberPlaylistsLib.Types.IPlaylist playlist)
        {
            if (string.IsNullOrEmpty(playlist.Filename)) {
                playlist.Filename = $"{playlist.Title}_{DateTime.Now:yyyyMMddHHmmss}";
            }
            var start = new Stopwatch();
            start.Start();
            await Task.Run(() =>
            {
                Logger.Info($"Filename : {playlist.Filename}");
                PlaylistManager.DefaultManager.StorePlaylist(playlist);
            });
            start.Stop();
            Logger.Info($"Save time : {start.ElapsedMilliseconds}ms");
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("cover")]
        private ImageView _cover;
        [UIComponent("check-box")]
        private object _checkBox;
        private ToggleSetting _toggle;

        [Inject]
        private LevelCollectionViewController _levelCollectionViewController;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // ファクトリー
        public class CellFactory : PlaceholderFactory<PlaylistCellEntity>
        {

        }
        #endregion
    }
}
