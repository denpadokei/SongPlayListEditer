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
using Zenject;

namespace SongPlayListEditer.Installer
{
    public class PlaylistEditerInstaller : InstallerBase
    {
        public override void InstallBindings()
        {
            this.Container.BindFactory<PlaylistCellEntity, PlaylistCellEntity.CellFactory>().AsCached();
            this.Container.BindViewController<SimplePlayListView>();
            this.Container.BindViewController<EditView>();
            this.Container.BindViewController<PlayListMenuView>();
            this.Container.BindFlowCoordinator<PlaylistEditorFlowCoordinator>();
            this.Container.Bind<BeatSaberUtility>().FromNewComponentOnNewGameObject("BSUlility").AsSingle();
            this.Container.Bind<IInitializable>().To<MenuUI>().FromNewComponentOnNewGameObject("MenuUI").AsSingle().NonLazy();
        }
    }
}
