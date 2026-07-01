using Maze.Common;
using UnityEngine;
using Zenject;

namespace Maze.Core.Diamonds
{
    public class Diamond : MonoBehaviour
    {
        [SerializeField] private string _playerTag = "Player";

        private IGameOrchestrator _orchestrator;
        private bool _collected;

        [Inject]
        public void Construct(IGameOrchestrator orchestrator) => _orchestrator = orchestrator;

        private void OnTriggerEnter(Collider other)
        {
            if (_collected || !other.CompareTag(_playerTag))
                return;

            _collected = true;
            gameObject.SetActive(false);
            _orchestrator.HandleDiamondCollected();
        }
    }
}
