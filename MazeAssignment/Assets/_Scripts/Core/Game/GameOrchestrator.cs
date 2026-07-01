using Maze.Common;
using Maze.Core.Diamonds;
using Maze.Core.GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze.Core.Game
{
    public class GameOrchestrator : IGameOrchestrator
    {
        private readonly IGameState _gameState;
        private readonly IGameAudio _gameAudio;
        private readonly IGameUi _gameUi;
        private readonly IDiamondCollection _diamondCollection;

        public GameOrchestrator(IGameState gameState, IGameAudio gameAudio, IGameUi gameUi, IDiamondCollection diamondCollection)
        {
            _gameState = gameState;
            _gameAudio = gameAudio;
            _gameUi = gameUi;
            _diamondCollection = diamondCollection;
        }

        public void HandleEnemyDetectedPlayer() => _gameAudio.PlayEnemyDetect();

        public void HandleEnemyCaughtPlayer()
        {
            if (_gameState.Phase != GamePhase.Playing)
                return;

            _gameAudio.PlayEnemyCatch();
            _gameState.NotifyPlayerDefeated();
            _gameUi.ShowDefeatScreen();
        }

        public void HandleDiamondCollected()
        {
            _diamondCollection.RegisterCollected();
            _gameAudio.PlayDiamondCollect();
            _gameUi.UpdateDiamondCount(_diamondCollection.CollectedCount, _diamondCollection.TotalCount);
        }

        public void HandlePlayerReachedExit()
        {
            if (_gameState.Phase != GamePhase.Playing)
                return;

            if (!_diamondCollection.AllCollected)
                return;

            _gameAudio.PlayVictory();
            _gameState.NotifyPlayerWon(_diamondCollection.CollectedCount);
            _gameUi.ShowVictoryScreen(_diamondCollection.CollectedCount, _diamondCollection.TotalCount);
        }

        public void HandleRestartRequested()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        public void HandleQuitRequested()
        {
            Application.Quit();
        }
    }
}
