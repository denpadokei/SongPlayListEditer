using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using SongPlayListEditer.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SongPlayListEditer.UI
{
    public class SimpleFlowCoordinater : FlowCoordinator
    {
        private SimplePlayListView _simplePlayList;

        protected override void DidActivate(bool firstActivation, ActivationType activationType)
        {
            //this.showBackButton = true;
            //this.ProvideInitialViewControllers(this._simplePlayList);
        }

        public void Dismiss()
        {
            // dismiss ourselves
            //BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            //base.BackButtonWasPressed(topViewController);

            var soloFlow = Resources.FindObjectsOfTypeAll<SoloFreePlayFlowCoordinator>().First();
            soloFlow.InvokeMethod<object, SoloFreePlayFlowCoordinator>("DismissFlowCoordinator", this, null, false);
            this.BackButtonWasPressed(null);
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            base.BackButtonWasPressed(topViewController);
        }

        private void Awake()
        {
            try {
                Logger.Info($"AwakeStart");
                //this._simplePlayList = BeatSaberUI.CreateViewController<SimplePlayListView>();
                Logger.Info($"Is playlist null? {this._simplePlayList == null}");
                //this._simplePlayList.SimpleFlowCoordinater = this;
                //this._songListView = BeatSaberUI.CreateViewController<SongListView>();
            }
            catch (Exception e) {
                Logger.Error(e);
            }
        }
    }
}
