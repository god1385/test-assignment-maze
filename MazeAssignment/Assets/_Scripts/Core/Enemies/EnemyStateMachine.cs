using System.Collections.Generic;

namespace Maze.Core.Enemies
{
    public class EnemyStateMachine
    {
        private readonly Dictionary<EnemyStateType, IEnemyState> _states = new();
        private IEnemyState _currentState;
        private bool _isStateActive;

        public EnemyStateType CurrentStateType => _currentState.Type;

        public void Initialize(IEnumerable<IEnemyState> states)
        {
            foreach (var state in states)
                _states.Add(state.Type, state);
        }

        public void ChangeState(EnemyStateType stateType)
        {
            if (_isStateActive && _currentState.Type == stateType)
                return;

            if (_isStateActive)
                _currentState.Exit();

            _currentState = _states[stateType];
            _currentState.Enter();
            _isStateActive = true;
        }

        public void Tick() => _currentState.Tick();
    }
}
