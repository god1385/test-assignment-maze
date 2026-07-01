using UnityEngine;

namespace Maze.Core.Enemies
{
    [System.Serializable]
    public class EnemySettings
    {
        [SerializeField] private float _patrolSpeed = 2.5f;
        [SerializeField] private float _chaseSpeed = 4.5f;
        [SerializeField] private float _returnSpeed = 3f;
        [SerializeField] private float _catchDistance = 1.2f;
        [SerializeField] private float _patrolPointReachDistance = 0.3f;
        [SerializeField] private float _chaseMemoryDuration = 3f;

        public float PatrolSpeed => _patrolSpeed;
        public float ChaseSpeed => _chaseSpeed;
        public float ReturnSpeed => _returnSpeed;
        public float CatchDistance => _catchDistance;
        public float PatrolPointReachDistance => _patrolPointReachDistance;
        public float ChaseMemoryDuration => _chaseMemoryDuration;
    }
}
