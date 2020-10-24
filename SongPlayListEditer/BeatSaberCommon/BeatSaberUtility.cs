using BeatSaberMarkupLanguage;
using BeatSaberPlaylistsLib;
using BS_Utils.Utilities;
using HMUI;
using SongPlayListEditer.Models;
using SongPlayListEditer.Statics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace SongPlayListEditer.BeatSaberCommon
{
    public class BeatSaberUtility : MonoBehaviour
    {
        [Inject]
        private LevelSelectionNavigationController _levelSelectionNavigationController;
        [Inject]
        private AnnotatedBeatmapLevelCollectionsViewController _annotatedBeatmapLevelCollectionsViewController;
        [Inject]
        private LevelCollectionViewController _levelCollectionViewController;

        public BeatSaberUtility()
        {
        }


        [Inject]
        public void Constractor()
        {
            this._levelCollectionViewController.didSelectLevelEvent += this.SelectLevelHandle;
        }

        public IPreviewBeatmapLevel CurrentPreviewBeatmapLevel { get; private set; }

        /// <summary>
        /// Internal BeatSaber song model
        /// </summary>
        public static BeatmapLevelsModel BeatmapLevelsModel { get; set; }
        public IBeatmapLevelPack CurrentPack { get; private set; }

        private void SelectLevelHandle(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            Logger.Info($"Selected Song : {arg2.songName}");
            CurrentPreviewBeatmapLevel = arg2;
        }

        /// <summary>
        /// Get the currently selected level pack within the LevelPackLevelViewController hierarchy.
        /// </summary>
        /// <returns></returns>
        public IBeatmapLevelPack GetCurrentSelectedLevelPack()
        {
            if (_levelSelectionNavigationController == null) {
                return null;
            }

            var pack = _levelSelectionNavigationController.GetPrivateField<IBeatmapLevelPack>("_levelPack");
            return pack;
        }

        /// <summary>
        /// Creates a copy of a template button and returns it.
        /// </summary>
        /// <param name="parent">The transform to parent the button to.</param>
        /// <param name="buttonTemplate">The name of the button to make a copy of. Example: "QuitButton", "PlayButton", etc.</param>
        /// <param name="anchoredPosition">The position the button should be anchored to.</param>
        /// <param name="sizeDelta">The size of the buttons RectTransform.</param>
        /// <param name="onClick">Callback for when the button is pressed.</param>
        /// <param name="buttonText">The text that should be shown on the button.</param>
        /// <param name="icon">The icon that should be shown on the button.</param>
        /// <returns>The newly created button.</returns>
        public static Button CreateUIButton(RectTransform parent, string buttonTemplate, Vector2 anchoredPosition, Vector2 sizeDelta, UnityAction onClick = null, string buttonText = "BUTTON", Sprite icon = null)
        {
            Button btn = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == buttonTemplate)), parent, false);
            btn.onClick = new Button.ButtonClickedEvent();
            if (onClick != null)
                btn.onClick.AddListener(onClick);
            btn.name = "CustomUIButton";

            (btn.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchoredPosition = anchoredPosition;
            (btn.transform as RectTransform).sizeDelta = sizeDelta;

            btn.SetButtonText(buttonText);
            if (icon != null)
                btn.SetButtonIcon(icon);

            return btn;
        }

        /// <summary>
        /// Clone a Unity Button into a Button we control.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buttonTemplate"></param>
        /// <param name="buttonInstance"></param>
        /// <returns></returns>
        static public Button CreateUIButton(RectTransform parent, Button buttonTemplate)
        {
            Button btn = UnityEngine.Object.Instantiate(buttonTemplate, parent, false);
            UnityEngine.Object.DestroyImmediate(btn.GetComponent<SignalOnUIButtonClick>());
            btn.onClick = new Button.ButtonClickedEvent();
            btn.name = "CustomUIButton";

            return btn;
        }

        /// <summary>
        /// Safely destroy existing hoverhint.
        /// </summary>
        /// <param name="button"></param>
        public static void DestroyHoverHint(RectTransform button)
        {
            HoverHint currentHoverHint = button.GetComponentsInChildren<HMUI.HoverHint>().First();
            if (currentHoverHint != null) {
                UnityEngine.GameObject.DestroyImmediate(currentHoverHint);
            }
        }

        public static IEnumerable<BeatSaberPlaylistsLib.Types.IPlaylist> GetLocalPlaylist()
        {
            foreach (var playlist in PlaylistManager.DefaultManager.GetAllPlaylists().OrderBy(x => x.Title)) {
                yield return playlist;
            }
        }

        /// <summary>
        /// Get the currently selected level collection from playlists.
        /// </summary>
        /// <returns></returns>
        private IPlaylist GetCurrentSelectedPlaylist()
        {
            if (_annotatedBeatmapLevelCollectionsViewController == null) {
                return null;
            }

            IPlaylist playlist = _annotatedBeatmapLevelCollectionsViewController.selectedAnnotatedBeatmapLevelCollection as IPlaylist;
            return playlist;
        }

        /// <summary>
        /// Helper to get either or playlist or 
        /// </summary>
        /// <returns></returns>
        public IAnnotatedBeatmapLevelCollection GetCurrentSelectedAnnotatedBeatmapLevelCollection()
        {
            IAnnotatedBeatmapLevelCollection collection = GetCurrentSelectedLevelPack();
            if (collection == null) {
                collection = GetCurrentSelectedPlaylist();
            }

            return collection;
        }

        /// <summary>
        /// Get Current levels from current level collection.
        /// </summary>
        /// <returns></returns>
        public IPreviewBeatmapLevel[] GetCurrentLevelCollectionLevels()
        {
            var levelCollection = GetCurrentSelectedAnnotatedBeatmapLevelCollection();
            if (levelCollection == null) {
                Logger.Debug("Current selected level collection is null for some reason...");
                return null;
            }

            return levelCollection.beatmapLevelCollection.beatmapLevels;
        }

        private void DidSelectLevelPack(LevelSelectionNavigationController controller, IBeatmapLevelPack beatmap)
        {
            Logger.Debug($"Pack name : {beatmap.packName}, Pack ID : {beatmap.packID}, Pack short name : {beatmap.shortPackName}");
            CurrentPack = beatmap;
        }

        public static HoverHint AddHintText(RectTransform parent, string text)
        {
            var hoverHint = parent.gameObject.AddComponent<HoverHint>();
            hoverHint.text = text;
            var hoverHintController = Resources.FindObjectsOfTypeAll<HoverHintController>().First();
            hoverHint.SetField("_hoverHintController", hoverHintController);
            return hoverHint;
        }
    }
}
