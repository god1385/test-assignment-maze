namespace Maze.Common
{
    public interface IGameOrchestrator
    {
        void HandleEnemyDetectedPlayer();
        void HandleEnemyCaughtPlayer();
        void HandleDiamondCollected();
        void HandlePlayerReachedExit();
        void HandleRestartRequested();
        void HandleQuitRequested();
    }
}
