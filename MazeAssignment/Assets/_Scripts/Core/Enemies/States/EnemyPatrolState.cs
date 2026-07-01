using UnityEngine;

namespace Maze.Core.Enemies.States
{
    public class EnemyPatrolState : IEnemyState
    {
        private readonly EnemyContext _context;
        private readonly EnemyStateMachine _stateMachine;

        public EnemyPatrolState(EnemyContext context, EnemyStateMachine stateMachine)
        {
            _context = context;
            _stateMachine = stateMachine;
        }

        public EnemyStateType Type => EnemyStateType.Patrol;

        public void Enter()
        {
            _context.Agent.speed = _context.Settings.PatrolSpeed;
            MoveToCurrentPatrolPoint();
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            if (_context.DetectionZone.IsPlayerInside)
            {
                _stateMachine.ChangeState(EnemyStateType.Chase);
                return;
            }

            if (HasReachedDestination())
                MoveToNextPatrolPoint();
        }

        private void MoveToCurrentPatrolPoint()
        {
            var patrolPoint = _context.PatrolPoints[_context.CurrentPatrolIndex];
            _context.Agent.SetDestination(patrolPoint.position);
        }

        private void MoveToNextPatrolPoint()
        {
            _context.CurrentPatrolIndex = (_context.CurrentPatrolIndex + 1) % _context.PatrolPoints.Length;
            MoveToCurrentPatrolPoint();
        }

        private bool HasReachedDestination()
        {
            if (_context.Agent.pathPending)
                return false;

            return _context.Agent.remainingDistance <= _context.Settings.PatrolPointReachDistance;
        }
    }
}
