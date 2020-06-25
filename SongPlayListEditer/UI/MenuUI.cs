using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA.Logging;
using BeatSaberMarkupLanguage;

namespace SongPlayListEditer.UI
{
    public class MenuUI : MonoBehaviour
    {
        private MainFlowCoordinator _mainFlowCoordinater;

        private void Awake()
        {
            this.CreateUI();
            //var levelListViewController = Resources.FindObjectsOfTypeAll<LevelCollectionViewController>().FirstOrDefault();
            //if (levelListViewController == null) {
            //    Logger.log.Error($"notfind {nameof(levelListViewController)}");
            //    return;
            //}
        }

        public void CreateUI()
        {
            var button = new MenuButton("SONG PLAYLIST EDITER", "Edit song playlist", this.ShowMainFlowCoodniator, true);
            MenuButtons.instance.RegisterButton(button);
        }

        private void ShowMainFlowCoodniator()
        {
            if (this._mainFlowCoordinater == null) {
                this._mainFlowCoordinater = BeatSaberUI.CreateFlowCoordinator<MainFlowCoordinator>();
            }

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this._mainFlowCoordinater);
        }
    }
}
