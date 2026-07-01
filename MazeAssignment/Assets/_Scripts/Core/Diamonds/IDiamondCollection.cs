namespace Maze.Core.Diamonds
{
    public interface IDiamondCollection
    {
        int CollectedCount { get; }
        int TotalCount { get; }
        bool AllCollected { get; }

        void SetTotal(int total);
        void RegisterCollected();
    }
}
