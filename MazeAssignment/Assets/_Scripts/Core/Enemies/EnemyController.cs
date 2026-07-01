using Maze.Common;
using Maze.Core.Enemies.States;
using Maze.Core.GameState;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Maze.Core.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private EnemyDetectionZone _detectionZone;
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private EnemySettings _settings;

        private NavMeshAgent _agent;
        private EnemyContext _context;
        private EnemyStateMachine _stateMachine;

        [Inject]
        public void Construct(IGameOrchestrator orchestrator, IGameState gameState)
        {
            _agent = GetComponent<NavMeshAgent>();

            var playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
            _detectionZone.Initialize(playerTransform);

            _context = new EnemyContext(playerTransform, _agent, _detectionZone, orchestrator, gameState, _patrolPoints, _settings);

            _stateMachine = new EnemyStateMachine();
            _stateMachine.Initialize(CreateStates());
        }

        private void Start()
        {
            EnsureAgentOnNavMesh();
            _stateMachine.ChangeState(EnemyStateType.Patrol);
        }

        private void EnsureAgentOnNavMesh()
        {
            if (_agent.isOnNavMesh)
                return;

            if (NavMesh.SamplePosition(transform.position, out var hit, 5f, NavMesh.AllAreas))
                _agent.Warp(hit.position);
        }

        private void Update()
        {
            if (_context.GameState.Phase != GamePhase.Playing)
                return;

            _stateMachine.Tick();
        }

        private IEnemyState[] CreateStates() =>
            new IEnemyState[]
            {
                new EnemyPatrolState(_context, _stateMachine),
                new EnemyChaseState(_context, _stateMachine),
                new EnemyReturnState(_context, _stateMachine)
            };
    }
}
