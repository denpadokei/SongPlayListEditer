using BeatSaberMarkupLanguage;
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

namespace SongPlayListEditer.BeatSaberCommon
{
    public static class BeatSaberUtility
    {
        public static LevelSelectionNavigationController LevelSelectionNavigationController { get; set; }

        public static LevelCollectionTableView LevelCollectionTableView { get; set; }

        public static FlowCoordinator LevelSelectionFlowCoordinator { get; set; }
        public static LevelFilteringNavigationController LevelFilteringNavigationController { get; set; }

        public static LevelCollectionViewController LevelCollectionViewController { get; set; }
        public static StandardLevelDetailViewController LevelDetailViewController{ get; set; }
        public static StandardLevelDetailView StandardLevelDetailView{ get; set; }

        public static BeatmapDifficultySegmentedControlController LevelDifficultyViewController{ get; set; }
        public static BeatmapCharacteristicSegmentedControlController BeatmapCharacteristicSelectionViewController{ get; set; }

        public static AnnotatedBeatmapLevelCollectionsViewController AnnotatedBeatmapLevelCollectionsViewController{ get; set; }

        public static RectTransform LevelCollectionTableViewTransform{ get; set; }

        public static Button TableViewPageUpButton{ get; set; }
        public static Button TableViewPageDownButton{ get; set; }

        public static RectTransform PlayContainer{ get; set; }
        public static RectTransform PlayButtons{ get; set; }

        public static Button PlayButton{ get; set; }

        public static Button FavoriteButton { get; set; }

        public static Button PracticeButton{ get; set; }

        public static SimpleDialogPromptViewController SimpleDialogPromptViewControllerPrefab{ get; set; }

        public static PlatformLeaderboardViewController PlatformLeaderboardViewController { get; set; }

        public static IDifficultyBeatmap CurrentBeatmap => LevelSelectionNavigationController.selectedDifficultyBeatmap;

        public static IBeatmapLevelPack CurrentPack { get; private set; }

        public static IPreviewBeatmapLevel CurrentPreviewBeatmapLevel { get; private set; }

        /// <summary>
        /// Internal BeatSaber song model
        /// </summary>
        public static BeatmapLevelsModel BeatmapLevelsModel { get; set; }

        public static void Initialize()
        {
            try {
                //Logger.Debug("CreateUI()");

                //// Determine the flow controller to use
                //FlowCoordinator flowCoordinator = null;
                //if (mode == MainMenuViewController.MenuButton.SoloFreePlay) {
                //    Logger.Debug("Entering SOLO mode...");
                //    flowCoordinator = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().First();
                //}
                //else if (mode == MainMenuViewController.MenuButton.Party) {
                //    Logger.Debug("Entering PARTY mode...");
                //    flowCoordinator = Resources.FindObjectsOfTypeAll<PartyFreePlayFlowCoordinator>().First();
                //}
                //else {
                //    Logger.Debug("Entering SOLO CAMPAIGN mode...");
                //    flowCoordinator = Resources.FindObjectsOfTypeAll<CampaignFlowCoordinator>().First();
                //    return;
                //}

                Logger.Debug("Entering SOLO mode...");
                var flowCoordinator = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().First();

                Logger.Debug($"{flowCoordinator.GetType().Namespace}");

                Logger.Debug("Done fetching Flow Coordinator for the appropriate mode...");

                Logger.Debug("Collecting all BeatSaberUI Elements...");

                LevelSelectionFlowCoordinator = flowCoordinator;



                // gather flow coordinator elements
                LevelSelectionNavigationController = LevelSelectionFlowCoordinator.GetPrivateField<LevelSelectionNavigationController>("_levelSelectionNavigationController");
                Logger.Debug($"Acquired LevelSelectionNavigationController [{LevelSelectionNavigationController.GetInstanceID()}]");

                LevelSelectionNavigationController.didSelectLevelPackEvent -= DidSelectLevelPack;
                LevelSelectionNavigationController.didSelectLevelPackEvent += DidSelectLevelPack;

                PlatformLeaderboardViewController = LevelSelectionFlowCoordinator.GetPrivateField<PlatformLeaderboardViewController>("_platformLeaderboardViewController");
                Logger.Debug($"Acquired PlatformLeaderboardViewController [{PlatformLeaderboardViewController.GetInstanceID()}]");

                LevelFilteringNavigationController = Resources.FindObjectsOfTypeAll<LevelFilteringNavigationController>().First();
                Logger.Debug($"Acquired LevelFilteringNavigationController [{LevelFilteringNavigationController.GetInstanceID()}]");

                LevelCollectionViewController = LevelSelectionNavigationController.GetPrivateField<LevelCollectionViewController>("_levelCollectionViewController");
                Logger.Debug($"Acquired LevelPackLevelsViewController [{LevelCollectionViewController.GetInstanceID()}]");
                LevelCollectionViewController.didSelectLevelEvent -= SelectLevelHandle;
                LevelCollectionViewController.didSelectLevelEvent += SelectLevelHandle;


                LevelDetailViewController = LevelSelectionNavigationController.GetPrivateField<StandardLevelDetailViewController>("_levelDetailViewController");
                Logger.Debug($"Acquired StandardLevelDetailViewController [{LevelDetailViewController.GetInstanceID()}]");

                LevelCollectionTableView = LevelCollectionViewController.GetPrivateField<LevelCollectionTableView>("_levelCollectionTableView");
                Logger.Debug($"Acquired LevelPackLevelsTableView [{LevelCollectionTableView.GetInstanceID()}]");

                StandardLevelDetailView = LevelDetailViewController.GetPrivateField<StandardLevelDetailView>("_standardLevelDetailView");
                Logger.Debug($"Acquired StandardLevelDetailView [{StandardLevelDetailView.GetInstanceID()}]");

                BeatmapCharacteristicSelectionViewController = Resources.FindObjectsOfTypeAll<BeatmapCharacteristicSegmentedControlController>().First();
                Logger.Debug($"Acquired BeatmapCharacteristicSegmentedControlController [{BeatmapCharacteristicSelectionViewController.GetInstanceID()}]");

                LevelDifficultyViewController = StandardLevelDetailView.GetPrivateField<BeatmapDifficultySegmentedControlController>("_beatmapDifficultySegmentedControlController");
                Logger.Debug($"Acquired BeatmapDifficultySegmentedControlController [{LevelDifficultyViewController.GetInstanceID()}]");

                LevelCollectionTableViewTransform = LevelCollectionTableView.transform as RectTransform;
                Logger.Debug($"Acquired TableViewRectTransform from LevelPackLevelsTableView [{LevelCollectionTableViewTransform.GetInstanceID()}]");

                AnnotatedBeatmapLevelCollectionsViewController = LevelFilteringNavigationController.GetPrivateField<AnnotatedBeatmapLevelCollectionsViewController>("_annotatedBeatmapLevelCollectionsViewController");

                TableView tableView = LevelCollectionTableView.GetPrivateField<TableView>("_tableView");
                TableViewPageUpButton = tableView.GetPrivateField<Button>("_pageUpButton");
                TableViewPageDownButton = tableView.GetPrivateField<Button>("_pageDownButton");
                Logger.Debug("Acquired Page Up and Down buttons...");

                PlayContainer = StandardLevelDetailView.GetComponentsInChildren<RectTransform>().First(x => x.name == "PlayContainer");
                PlayButtons = PlayContainer.GetComponentsInChildren<RectTransform>().First(x => x.name == "PlayButtons");

                PlayButton = Resources.FindObjectsOfTypeAll<Button>().First(x => x.name == "PlayButton");

                PracticeButton = PlayButtons.GetComponentsInChildren<Button>().First(x => x.name == "PracticeButton");

                SimpleDialogPromptViewControllerPrefab = Resources.FindObjectsOfTypeAll<SimpleDialogPromptViewController>().First();

                BeatmapLevelsModel = Resources.FindObjectsOfTypeAll<BeatmapLevelsModel>().First();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }

        private static void SelectLevelHandle(LevelCollectionViewController arg1, IPreviewBeatmapLevel arg2)
        {
            Logger.Info($"Selected Song : {arg2.songName}");
            CurrentPreviewBeatmapLevel = arg2;
        }

        /// <summary>
        /// Get the currently selected level pack within the LevelPackLevelViewController hierarchy.
        /// </summary>
        /// <returns></returns>
        public static IBeatmapLevelPack GetCurrentSelectedLevelPack()
        {
            if (LevelSelectionNavigationController == null) {
                return null;
            }

            var pack = LevelSelectionNavigationController.GetPrivateField<IBeatmapLevelPack>("_levelPack");
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

        public static IEnumerable<Playlist> GetLocalPlaylist()
        {
            Logger.Info($"Playlists Path : [{FilePathName.PlaylistsFolderPath}]");

            foreach (var playlistpath in Directory.EnumerateFiles(FilePathName.PlaylistsFolderPath).OrderBy(x => x)) {
                var playlist = Playlist.LoadPlaylist(playlistpath);
                yield return playlist;
            }
        }

        /// <summary>
        /// Get the currently selected level collection from playlists.
        /// </summary>
        /// <returns></returns>
        private static IPlaylist GetCurrentSelectedPlaylist()
        {
            if (AnnotatedBeatmapLevelCollectionsViewController == null) {
                return null;
            }

            IPlaylist playlist = AnnotatedBeatmapLevelCollectionsViewController.selectedAnnotatedBeatmapLevelCollection as IPlaylist;
            return playlist;
        }

        /// <summary>
        /// Helper to get either or playlist or 
        /// </summary>
        /// <returns></returns>
        public static IAnnotatedBeatmapLevelCollection GetCurrentSelectedAnnotatedBeatmapLevelCollection()
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
        public static IPreviewBeatmapLevel[] GetCurrentLevelCollectionLevels()
        {
            var levelCollection = GetCurrentSelectedAnnotatedBeatmapLevelCollection();
            if (levelCollection == null) {
                Logger.Debug("Current selected level collection is null for some reason...");
                return null;
            }

            return levelCollection.beatmapLevelCollection.beatmapLevels;
        }

        private static void DidSelectLevelPack(LevelSelectionNavigationController controller, IBeatmapLevelPack beatmap)
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
