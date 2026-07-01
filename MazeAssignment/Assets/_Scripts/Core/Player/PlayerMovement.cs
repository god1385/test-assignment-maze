using Maze.Common;
using UnityEngine;

namespace Maze.Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 4f;
        [SerializeField] private float _sprintSpeed = 7f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _controller;
        private IPlayerInput _input;
        private float _verticalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputReader>();
        }

        private void Update() => ApplyMovement();

        private void ApplyMovement()
        {
            var moveInput = _input.Move;
            var moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            var speed = _input.IsSprinting ? _sprintSpeed : _walkSpeed;

            if (_controller.isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = -2f;

            _verticalVelocity += _gravity * Time.deltaTime;

            var velocity = moveDirection * speed;
            velocity.y = _verticalVelocity;

            _controller.Move(velocity * Time.deltaTime);
        }
    }
}
