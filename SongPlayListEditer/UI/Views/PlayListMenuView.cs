﻿using System;
using System.Collections.Generic;
using System.Linq;

using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;


namespace SongPlayListEditer.UI.Views
{
    internal class PlayListMenuView : BSMLResourceViewController
    {
        // For this method of setting the ResourceName, this class must be the first class in the file.
        public override string ResourceName => string.Join(".", GetType().Namespace, "PlayListMenuView.bsml");

        public MainFlowCoordinator MainFlowCoordinater { get; internal set; }
    }
}
