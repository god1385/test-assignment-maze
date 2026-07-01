namespace Maze.Core.Enemies
{
    public interface IEnemyState
    {
        EnemyStateType Type { get; }
        void Enter();
        void Exit();
        void Tick();
    }
}
