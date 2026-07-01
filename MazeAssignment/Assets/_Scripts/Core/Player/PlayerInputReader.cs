using Maze.Common;
using UnityEngine;

namespace Maze.Core.Player
{
    public class PlayerInputReader : MonoBehaviour, IPlayerInput
    {
        private PlayerInputActions _inputActions;

        public Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>();

        public Vector2 Look => _inputActions.Player.Look.ReadValue<Vector2>();

        public bool IsSprinting => _inputActions.Player.Sprint.IsPressed();

        private void Awake() => _inputActions = new PlayerInputActions();

        private void OnEnable() => _inputActions.Player.Enable();

        private void OnDisable() => _inputActions.Player.Disable();

        private void OnDestroy() => _inputActions.Dispose();
    }
}
