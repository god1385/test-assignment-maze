using Maze.Core.GameState;
using Maze.Utilities;
using UnityEngine;
using Zenject;

namespace Maze.Core.Player
{
    public class PlayerDefeatHandler : MonoBehaviour
    {
        private IGameState _gameState;
        private PlayerInputReader _inputReader;
        private PlayerMovement _movement;
        private PlayerCameraLook _cameraLook;

        [Inject]
        public void Construct(IGameState gameState)
        {
            _gameState = gameState;
            _gameState.Defeated += DisableGameplay;
            _gameState.Victory += OnVictory;
        }

        private void Awake()
        {
            _inputReader = GetComponent<PlayerInputReader>();
            _movement = GetComponent<PlayerMovement>();
            _cameraLook = GetComponent<PlayerCameraLook>();
        }

        private void OnDestroy()
        {
            _gameState.Defeated -= DisableGameplay;
            _gameState.Victory -= OnVictory;
        }

        private void OnVictory(int collectedDiamonds) => DisableGameplay();

        private void DisableGameplay()
        {
            _inputReader.enabled = false;
            _movement.enabled = false;
            _cameraLook.enabled = false;
            GameCursor.Unlock();
        }
    }
}
