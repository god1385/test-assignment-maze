using Maze.Common;
using Maze.Core.Game;
using Maze.Core.Audio;
using Maze.Core.Diamonds;
using Maze.Core.GameState;
using Maze.Core.Player;
using Maze.Core.UI;
using UnityEngine;
using Zenject;

namespace Maze.Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerRoot;
        [SerializeField] private GameAudio _gameAudio;
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private DiamondCatalog _diamondCatalog;

        public override void InstallBindings()
        {
            BindGameState();
            BindDiamonds();
            BindOrchestrator();
            BindPlayer();
        }

        private void BindDiamonds()
        {
            Container.BindInterfacesAndSelfTo<DiamondCollection>().AsSingle();
            Container.BindInterfacesAndSelfTo<DiamondCatalog>()
                .FromInstance(_diamondCatalog)
                .AsSingle()
                .NonLazy();
        }

        private void BindGameState()
        {
            Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
        }

        private void BindOrchestrator()
        {
            Container.Bind<IGameAudio>().FromInstance(_gameAudio).AsSingle();
            Container.Bind<IGameUi>().FromInstance(_gameUi).AsSingle();
            Container.BindInterfacesAndSelfTo<GameOrchestrator>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<IPlayerInput>().To<PlayerInputReader>().FromComponentOn(_playerRoot.gameObject).AsSingle();
        }
    }
}
