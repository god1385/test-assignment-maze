using UnityEngine;

namespace Maze.Core.Enemies.States
{
    public class EnemyReturnState : IEnemyState
    {
        private readonly EnemyContext _context;
        private readonly EnemyStateMachine _stateMachine;

        public EnemyReturnState(EnemyContext context, EnemyStateMachine stateMachine)
        {
            _context = context;
            _stateMachine = stateMachine;
        }

        public EnemyStateType Type => EnemyStateType.Return;

        public void Enter()
        {
            _context.Agent.speed = _context.Settings.ReturnSpeed;
            _context.ReturnPatrolIndex = FindNearestPatrolPointIndex();
            _context.Agent.SetDestination(_context.PatrolPoints[_context.ReturnPatrolIndex].position);
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

            if (!HasReachedDestination())
                return;

            _context.CurrentPatrolIndex = _context.ReturnPatrolIndex;
            _stateMachine.ChangeState(EnemyStateType.Patrol);
        }

        private int FindNearestPatrolPointIndex()
        {
            var agentPosition = _context.Agent.transform.position;
            var nearestIndex = 0;
            var nearestDistance = float.MaxValue;

            for (var i = 0; i < _context.PatrolPoints.Length; i++)
            {
                var distance = Vector3.Distance(agentPosition, _context.PatrolPoints[i].position);

                if (distance >= nearestDistance)
                    continue;

                nearestDistance = distance;
                nearestIndex = i;
            }

            return nearestIndex;
        }

        private bool HasReachedDestination()
        {
            if (_context.Agent.pathPending)
                return false;

            return _context.Agent.remainingDistance <= _context.Settings.PatrolPointReachDistance;
        }
    }
}
