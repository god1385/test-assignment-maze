using UnityEngine;

namespace Maze.Core.Enemies.States
{
    public class EnemyChaseState : IEnemyState
    {
        private readonly EnemyContext _context;
        private readonly EnemyStateMachine _stateMachine;

        private float _chaseTimer;

        public EnemyChaseState(EnemyContext context, EnemyStateMachine stateMachine)
        {
            _context = context;
            _stateMachine = stateMachine;
        }

        public EnemyStateType Type => EnemyStateType.Chase;

        public void Enter()
        {
            _context.Agent.speed = _context.Settings.ChaseSpeed;
            _context.Agent.isStopped = false;
            _chaseTimer = 0f;
            _context.Agent.SetDestination(_context.PlayerTransform.position);
            _context.Orchestrator.HandleEnemyDetectedPlayer();
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            _chaseTimer += Time.deltaTime;

            if (_chaseTimer >= _context.Settings.ChaseMemoryDuration)
            {
                _stateMachine.ChangeState(EnemyStateType.Return);
                return;
            }

            var playerPosition = _context.PlayerTransform.position;

            if (_context.DetectionZone.IsPlayerInside)
                _context.Agent.SetDestination(playerPosition);

            if (HasCaughtPlayer(playerPosition))
                _context.Orchestrator.HandleEnemyCaughtPlayer();
        }

        private bool HasCaughtPlayer(Vector3 playerPosition) =>
            Vector3.Distance(_context.Agent.transform.position, playerPosition) <= _context.Settings.CatchDistance;
    }
}
