namespace Maze.Common
{
    public interface IGameUi
    {
        void UpdateDiamondCount(int collected, int total);
        void ShowDefeatScreen();
        void ShowVictoryScreen(int collectedDiamonds, int total);
    }
}
