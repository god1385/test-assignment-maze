using System;

namespace Maze.Core.GameState
{
    public class GameSession : IGameState
    {
        public GamePhase Phase { get; private set; } = GamePhase.Playing;
        public event Action Defeated = delegate { };
        public event Action<int> Victory = delegate { };

        public void NotifyPlayerDefeated()
        {
            if (Phase != GamePhase.Playing)
                return;

            Phase = GamePhase.Defeat;
            Defeated.Invoke();
        }

        public void NotifyPlayerWon(int collectedDiamonds)
        {
            if (Phase != GamePhase.Playing)
                return;

            Phase = GamePhase.Victory;
            Victory.Invoke(collectedDiamonds);
        }
    }
}
