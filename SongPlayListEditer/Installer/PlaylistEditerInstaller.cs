using SiraUtil;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Interfaces;
using SongPlayListEditer.Models;
using SongPlayListEditer.UI;
using SongPlayListEditer.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SongPlayListEditer.Installer
{
    public class PlaylistEditerInstaller : InstallerBase
    {
        public override void InstallBindings()
        {
            this.Container.BindFactory<BeatSaberPlaylistsLib.Types.IPlaylist, IPreviewBeatmapLevel, PlaylistCellEntity, PlaylistCellEntity.CellFactory>().AsCached();
            this.Container.BindInterfacesAndSelfTo<SimplePlayListView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            this.Container.Bind<BeatSaberUtility>().FromNewComponentOnNewGameObject("BSUlility").AsSingle();
            this.Container.BindInterfacesAndSelfTo<MenuUI>().FromNewComponentOnNewGameObject("MenuUI").AsCached().NonLazy();
        }
    }
}
