using Maze.Common;
using Maze.Core.Diamonds;
using UnityEngine;
using Zenject;

namespace Maze.Core.Maze
{
    [RequireComponent(typeof(BoxCollider))]
    public class MazeExit : MonoBehaviour
    {
        [SerializeField] private BoxCollider _triggerCollider;
        [SerializeField] private string _playerTag = "Player";

        private IGameOrchestrator _orchestrator;
        private IDiamondCollection _diamondCollection;
        private bool _isOpen;

        [Inject]
        public void Construct(IGameOrchestrator orchestrator, IDiamondCollection diamondCollection)
        {
            _orchestrator = orchestrator;
            _diamondCollection = diamondCollection;
        }

        private void Awake()
        {
            if (_triggerCollider == null)
                _triggerCollider = GetComponent<BoxCollider>();

            _triggerCollider.isTrigger = false;
        }

        private void Update()
        {
            if (_isOpen || !_diamondCollection.AllCollected)
                return;

            _isOpen = true;
            _triggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayerCollider(other))
                return;

            _orchestrator.HandlePlayerReachedExit();
        }

        private bool IsPlayerCollider(Collider other) => other.CompareTag(_playerTag);
    }
}
