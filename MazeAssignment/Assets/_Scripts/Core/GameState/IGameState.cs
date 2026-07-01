using System;

namespace Maze.Core.GameState
{
    public interface IGameState
    {
        GamePhase Phase { get; }
        event Action Defeated;
        event Action<int> Victory;

        void NotifyPlayerDefeated();
        void NotifyPlayerWon(int collectedDiamonds);
    }
}
