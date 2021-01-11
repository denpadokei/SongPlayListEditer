using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberPlaylistsLib;
using HMUI;
using SongPlayListEditer.Bases;
using SongPlayListEditer.Configuration;
using SongPlayListEditer.DataBases;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Interfaces;
using SongPlayListEditer.Statics;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
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
                        this._toggle.toggle.isOn = this.CurrentPlaylist.Any(x => !string.IsNullOrEmpty(x.LevelId) && x.LevelId == this.BeatMap?.levelID);
                        break;
                    default:
                        this._toggle.toggle.isOn = this.CurrentPlaylist.Any(x => x.Hash?.ToUpper() == this.BeatMap?.GetBeatmapHash().ToUpper());
                        break;
                }
                this._toggle.toggle.onValueChanged.AddListener(this.OnToggleChange);
            });
            HMMainThreadDispatcher.instance?.Enqueue(this.SetCover());

        }

        public void SelectedCell()
        {
            this._toggle.toggle.isOn = !this._toggle.toggle.isOn;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド

        private async Task AddSong()
        {
            if (this.BeatMap == null || this.CurrentPlaylist == null) {
                Logger.Debug($"this.BeatMap is null? : {this.BeatMap == null}");
                Logger.Debug($"this.CurrentPlaylist is null? : {this.CurrentPlaylist == null}");
                return;
            }
            this.CurrentPlaylist.AllowDuplicates = true;
            switch (this.SongType) {
                case SongTypeMode.Custom:
                    
                    var addTargetHash = this.BeatMap?.GetBeatmapHash().ToUpper();
                    if (this.CurrentPlaylist.Any(x => x.Hash?.ToUpper() == addTargetHash)) {
                        return;
                    }
                    if (PluginConfig.Instance.IsSaveWithKey) {
                        this.CurrentPlaylist.Add(addTargetHash, this.BeatMap.songName, await BeatSarverData.GetBeatMapKey(addTargetHash), this.BeatMap.levelAuthorName);
                    }
                    else {
                        this.CurrentPlaylist.Add(addTargetHash, this.BeatMap.songName, null, this.BeatMap.levelAuthorName);
                    }
                    break;
                case SongTypeMode.Official:
                    if (this.CurrentPlaylist.Any(x => x.LevelId?.ToUpper() == this.BeatMap.levelID.ToUpper())) {
                        return;
                    }
                    var officalSong = new OfficialSongEntity()
                    {
                        LevelId = this.BeatMap.levelID,
                        Name = this.BeatMap.songName,
                        DateAdded = DateTime.Now
                    };
                    officalSong.AddIdentifierFlag(BeatSaberPlaylistsLib.Types.Identifier.LevelId);
                    this.CurrentPlaylist.Add(officalSong);
                    break;
                default:
                    break;
            }
        }

        private void RemoveSong()
        {
            if (this.BeatMap == null || this.CurrentPlaylist == null) {
                return;
            }
            switch (this.SongType) {
                case SongTypeMode.Custom:
                    this.CurrentPlaylist?.TryRemoveByHash(this.BeatMap?.GetBeatmapHash());
                    break;
                case SongTypeMode.Official:
                    var levelID = this.BeatMap.levelID.ToUpper();
                    this.CurrentPlaylist?.Remove(this.CurrentPlaylist?.FirstOrDefault(x => x.LevelId?.ToUpper() == levelID));
                    break;
                default:
                    break;
            }
        }


        private IEnumerator SetCover()
        {
            if (Base64Sprites.CashedTextuer.TryGetValue(this.CurrentPlaylist.Filename, out var tex)) {
                this._cover.sprite = Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f));
            }
            else {
                var stream = this.CurrentPlaylist.GetCoverStream();
                tex = Base64Sprites.StreamToTextuer2D(stream);
                Base64Sprites.CashedTextuer.TryAdd(this.CurrentPlaylist.Filename, tex);
                this._cover.sprite = Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f));
            }
            yield return null;
        }

        private async Task SavePlaylist()
        {
            if (string.IsNullOrEmpty(this.CurrentPlaylist.Filename)) {
                this.CurrentPlaylist.Filename = $"{this.CurrentPlaylist.Title}_{DateTime.Now:yyyyMMddHHmmss}";
            }
            var start = new Stopwatch();
            start.Start();
            await Task.Run(() =>
            {
                Logger.Info($"Filename : {this.CurrentPlaylist.Filename}");
                PlaylistManager.DefaultManager.StorePlaylist(this.CurrentPlaylist);
            });
            start.Stop();
            Logger.Info($"Save time : {start.ElapsedMilliseconds}ms");
        }

        private async void OnToggleChange(bool value)
        {
            Logger.Debug($"change check box");
            Logger.Debug($"value : {value}");
            if (value) {
                await this.AddSong();
            }
            else {
                this.RemoveSong();

            }
            await this.SavePlaylist();

            this.SubInfo = $"Song count - {this.CurrentPlaylist.Count}";
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("cover")]
        private ImageView _cover;
        [UIComponent("check-box")]
        private object _checkBox;
        private ToggleSetting _toggle;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        public PlaylistCellEntity(BeatSaberPlaylistsLib.Types.IPlaylist playlists, IPreviewBeatmapLevel beatmapLevel)
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
        #region // ファクトリー
        public class CellFactory : PlaceholderFactory<BeatSaberPlaylistsLib.Types.IPlaylist, IPreviewBeatmapLevel, PlaylistCellEntity>
        {

        }
        #endregion
    }
}
