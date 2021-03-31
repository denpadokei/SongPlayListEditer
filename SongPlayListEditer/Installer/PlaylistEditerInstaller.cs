using SiraUtil;
using SongPlayListEditer.BeatSaberCommon;
using SongPlayListEditer.Models;
using SongPlayListEditer.UI;
using SongPlayListEditer.UI.Views;
using Zenject;

namespace SongPlayListEditer.Installer
{
    public class PlaylistEditerInstaller : InstallerBase
    {

        public override void InstallBindings()
        {
            this.Container.BindFactory<BeatSaberPlaylistsLib.Types.IPlaylist, IPreviewBeatmapLevel, PlaylistCellEntity, PlaylistCellEntity.CellFactory>().AsCached();
            this.Container.BindInterfacesAndSelfTo<SimplePlayListView>().FromNewComponentAsViewController().AsSingle().NonLazy();

            this.Container.BindInterfacesAndSelfTo<BeatSaberUtility>().FromNewComponentOnNewGameObject("BSUlility").AsSingle();

            this.Container.BindInterfacesAndSelfTo<EditView>().FromNewComponentAsViewController().AsSingle();
            this.Container.BindInterfacesAndSelfTo<PlayListMenuView>().FromNewComponentAsViewController().AsSingle();
            this.Container.BindInterfacesAndSelfTo<PlaylistEditorFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(PlaylistEditorFlowCoordinator)).AsSingle().NonLazy();

            this.Container.BindInterfacesAndSelfTo<SettingView>().FromNewComponentAsViewController().AsSingle().NonLazy();
        }
    }
}
