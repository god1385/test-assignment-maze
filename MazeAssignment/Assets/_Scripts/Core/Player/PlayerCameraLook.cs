using Maze.Common;
using Maze.Utilities;
using UnityEngine;

namespace Maze.Core.Player
{
    public class PlayerCameraLook : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _mouseSensitivity = 0.15f;
        [SerializeField] private float _maxPitch = 80f;

        private IPlayerInput _input;
        private float _yaw;
        private float _pitch;

        private void Awake()
        {
            _input = GetComponent<PlayerInputReader>();
            _yaw = transform.eulerAngles.y;
            _pitch = NormalizePitch(_cameraTransform.localEulerAngles.x);
        }

        private void OnEnable() => GameCursor.Lock();

        private void OnDisable() => GameCursor.Unlock();

        private void Update() => ApplyLook();

        private void ApplyLook()
        {
            var lookInput = _input.Look;

            _yaw += lookInput.x * _mouseSensitivity;
            _pitch -= lookInput.y * _mouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, -_maxPitch, _maxPitch);

            transform.rotation = Quaternion.Euler(0f, _yaw, 0f);
            _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        }

        private static float NormalizePitch(float eulerX) =>
            eulerX > 180f ? eulerX - 360f : eulerX;
    }
}
