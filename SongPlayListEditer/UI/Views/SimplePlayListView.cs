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
using HMUI;
using SimpleJSON;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Extentions;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using UnityEngine;

namespace SongPlayListEditer.UI.Views
{
    internal class SimplePlayListView : BSMLResourceViewController
    {
        /// <summary>
        /// For this method of setting the ResourceName, this class must be the first class in the file. 
        /// </summary>
        public override string ResourceName => string.Join(".", GetType().Namespace, "SimplePlayListView.bsml");

        public static SimplePlayListView Instance { get; private set; }

        public Playlist CurrentPlaylist { get; private set; }

        private PlaylistEdierDomain _domain = new PlaylistEdierDomain();

        private readonly static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private void Awake()
        {
            Logger.Info("Start Awake");
            this.AddButonText = "Add";
            Logger.Info("Finish Awake");
        }

        protected override void DidActivate(bool firstActivation, ActivationType type)
        {
            base.DidActivate(firstActivation, type);

            Instance = this;
        }

        public SimpleFlowCoordinater SimpleFlowCoordinater { get; internal set; }

        [UIComponent("playlists")]
        public CustomListTableData playlists;

        [UIValue("add-text")]
        public string AddButonText;

        [UIAction("#post-parse")]
        public void CreateList()
        {
            try {
                Logger.Info("Create List");
                this.playlists.data.Clear();
                this.playlists.data.AddRange(BeatSaberUtility.GetLocalPlaylist().Select(x => new CustomListTableData.CustomCellInfo(x.playlistTitle, $"Song count : {x.songs.Count}", Base64Sprites.Base64ToTexture2D(x.image))));
                Logger.Info($"Playlists count : {this.playlists.data.Count}");
                this.playlists.tableView.ReloadData();
                this.playlists.tableView.SelectCellWithIdx(-1);
                Logger.Info("Created List");
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        [UIAction("add-click")]
        public async Task AddClick()
        {
            try {
                await _semaphore.WaitAsync();

                if (this.AddButonText == "Add" && !this.CurrentPlaylist.songs.Any(x => x.levelId == BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash())) {
                    this.CurrentPlaylist.songs.Add(new PlaylistSong()
                    {
                        key = "",
                        songName = BeatSaberUtility.CurrentPreviewBeatmapLevel.songName,
                        hash = BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash(),
                    });
                    await this._domain.SavePlaylist(this.CurrentPlaylist);
                }
                else {
                    this.CurrentPlaylist.songs.RemoveAll(x => x.hash == BeatSaberUtility.CurrentPreviewBeatmapLevel.GetBeatmapHash());
                    await this._domain.SavePlaylist(this.CurrentPlaylist);
                }
                this.playlists.tableView.ReloadData();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
            finally {
                _semaphore.Release();
            }
        }

        [UIAction("cancel-click")]
        public void CancelClick()
        {
            this.SimpleFlowCoordinater.Dismiss();
        }

        [UIAction("current")]
        public void SelectedPlaylist(TableView table, int selectRow)
        {
            var playlists = BeatSaberUtility.GetLocalPlaylist().ToArray();
            this.CurrentPlaylist = playlists[selectRow];
            var isContain = this.CurrentPlaylist.songs.Any(x => x.hash == BeatSaberUtility.CurrentPreviewBeatmapLevel.levelID);
            if (isContain) {
                this.AddButonText = "Delete";
            }
            else {
                this.AddButonText = "Add";
            }
        }
    }
}
