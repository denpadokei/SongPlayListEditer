using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using SongPlayListEditer.Bases;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static BeatSaberMarkupLanguage.Components.CustomListTableData;

namespace SongPlayListEditer.UI.Views
{
    [HotReload]
    public class PlayListMenuView : ViewContlollerBindableBase
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        // For this method of setting the ResourceName, this class must be the first class in the file.

        /// <summary>編集ボタンのインタラクティブ を取得、設定</summary>
        private bool isEnableEditButton_;
        /// <summary>編集ボタンのインタラクティブ を取得、設定</summary>
        [UIValue("edit-button-interactive")]
        public bool IsEnableEditButton
        {
            get => this.isEnableEditButton_;

            set => this.SetProperty(ref this.isEnableEditButton_, value);
        }

        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // イベント
        public event Action ShowAddEvent;
        public event Action ShowEditEvent;
        public event Action<BeatSaberPlaylistsLib.Types.IPlaylist> SelectedCell;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // ボタン用メソッド
        [UIAction("add-click")]
        public void AddClick()
        {
            try {
                Logger.Info("Clicked create button.");
                this.ShowAddEvent?.Invoke();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        [UIAction("edit-click")]
        public void EditClick()
        {
            try {
                Logger.Info("Clicked edit button");
                this.ShowEditEvent?.Invoke();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
            this.CreateList();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        public void CreateList()
        {
            _ = this.CreateListAsync();
            this.SelectedCell?.Invoke(null);
            this.IsEnableEditButton = false;
        }

        public async Task CreateListAsync()
        {
            await _createlistSemaphore.WaitAsync();

            var start = new Stopwatch();
            start.Start();
            try {
                Logger.Info("Create List");
                this._playlists?.tableView?.SelectCellWithIdx(-1);
                this._playlists?.data?.Clear();

                foreach (var playlist in BeatSaberUtility.GetLocalPlaylist()) {
                    var lockedPlaylists = new LockedPlaylistEntity().Read();
                    var title = lockedPlaylists.LockedPlaylists.Any(x => Regex.IsMatch(x, $"^{playlist.Filename}$")) ? $"<color=yellow>{playlist.Title}</color>" : playlist.Title;

                    if (Base64Sprites.CashedTextuer.TryGetValue(playlist.Filename, out var tex)) {

                        HMMainThreadDispatcher.instance?.Enqueue(() =>
                        {
                            this._playlists?.data.Add(new CustomCellInfo(title, $"Song count-{playlist.Count}", Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f))));
                        });
                    }
                    else {
                        var coverstream = playlist.GetCoverStream();
                        tex = Base64Sprites.StreamToTextuer2D(coverstream);
                        Base64Sprites.CashedTextuer.AddOrUpdate(playlist.Filename, tex, (s, t) => tex);
                        var sprite = Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f));
                        HMMainThreadDispatcher.instance?.Enqueue(() =>
                        {
                            this._playlists?.data.Add(new CustomCellInfo(title, $"Song count-{playlist.Count}", sprite));
                        });
                    }
                }
                Logger.Info($"Playlists count : {this._playlists.data.Count}");
                HMMainThreadDispatcher.instance?.Enqueue(() =>
                {
                    this._playlists?.tableView?.ReloadData();
                });
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

        [UIAction("select-cell")]
        private void SelectCell(TableView tableView, int cellindex)
        {
            Logger.Info("Selected Cell");
            this.SelectedCell?.Invoke(BeatSaberUtility.GetLocalPlaylist().ToArray()[cellindex]);
            this.IsEnableEditButton = true;
        }

        [UIAction("all-lock")]
        private void AllLock()
        {
            var lockedPlaylists = new LockedPlaylistEntity().Read();
            lockedPlaylists.Add(BeatSaberUtility.GetLocalPlaylist().Select(x => x.Filename).ToArray());
            lockedPlaylists.Save();
            this.CreateList();
        }

        [UIAction("all-unlock")]
        private void Allunlock()
        {
            var lockedPlaylists = new LockedPlaylistEntity().Read();
            lockedPlaylists.Remove(BeatSaberUtility.GetLocalPlaylist().Select(x => x.Filename).ToArray());
            lockedPlaylists.Save();
            this.CreateList();
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        [UIComponent("playlists")]
        private readonly CustomListTableData _playlists;

        private static readonly SemaphoreSlim _createlistSemaphore = new SemaphoreSlim(1, 1);
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        #endregion
    }
}
