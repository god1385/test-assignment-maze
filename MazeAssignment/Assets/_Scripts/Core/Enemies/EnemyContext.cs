using Maze.Common;
using Maze.Core.GameState;
using UnityEngine;
using UnityEngine.AI;

namespace Maze.Core.Enemies
{
    public class EnemyContext
    {
        public EnemyContext(Transform playerTransform, NavMeshAgent agent, EnemyDetectionZone detectionZone, IGameOrchestrator orchestrator, IGameState gameState, Transform[] patrolPoints, EnemySettings settings)
        {
            PlayerTransform = playerTransform;
            Agent = agent;
            DetectionZone = detectionZone;
            Orchestrator = orchestrator;
            GameState = gameState;
            PatrolPoints = patrolPoints;
            Settings = settings;
        }

        public Transform PlayerTransform { get; }
        public NavMeshAgent Agent { get; }
        public EnemyDetectionZone DetectionZone { get; }
        public IGameOrchestrator Orchestrator { get; }
        public IGameState GameState { get; }
        public Transform[] PatrolPoints { get; }
        public EnemySettings Settings { get; }

        public int CurrentPatrolIndex { get; set; }
        public int ReturnPatrolIndex { get; set; }
    }
}
