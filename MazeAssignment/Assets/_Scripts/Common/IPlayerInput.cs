using UnityEngine;

namespace Maze.Common
{
    public interface IPlayerInput
    {
        Vector2 Move { get; }
        Vector2 Look { get; }
        bool IsSprinting { get; }
    }
}
