using UnityEngine;

namespace Maze.Core.Enemies
{
    [RequireComponent(typeof(BoxCollider))]
    public class EnemyDetectionZone : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstructionMask = ~0;
        [SerializeField] private float _targetHeightOffset = 1f;
        [SerializeField] private float _proximityRadius = 2f;
        [SerializeField] private string _playerTag = "Player";

        private readonly RaycastHit[] _lineOfSightHits = new RaycastHit[8];
        private Transform _playerTransform;
        private Transform _ownerRoot;
        private bool _isPlayerOverlapping;

        public bool IsPlayerInside { get; private set; }

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _ownerRoot = GetComponentInParent<EnemyController>().transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsPlayerCollider(other))
                _isPlayerOverlapping = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsPlayerCollider(other))
                _isPlayerOverlapping = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsPlayerCollider(other))
                _isPlayerOverlapping = false;
        }

        private void LateUpdate() =>
            IsPlayerInside = (_isPlayerOverlapping || IsWithinProximity()) && HasLineOfSight();

        private bool IsWithinProximity()
        {
            if (_proximityRadius <= 0f)
                return false;

            return (_playerTransform.position - _ownerRoot.position).sqrMagnitude
                <= _proximityRadius * _proximityRadius;
        }

        private bool HasLineOfSight()
        {
            var origin = transform.position;
            var target = _playerTransform.position + Vector3.up * _targetHeightOffset;
            var direction = target - origin;
            var distance = direction.magnitude;

            if (distance <= Mathf.Epsilon)
                return true;

            var hitCount = Physics.RaycastNonAlloc(origin, direction / distance, _lineOfSightHits, distance, _obstructionMask, QueryTriggerInteraction.Ignore);

            for (var i = 0; i < hitCount; i++)
            {
                var hitTransform = _lineOfSightHits[i].transform;

                if (IsPlayerCollider(hitTransform) || IsOwnerTransform(hitTransform))
                    continue;

                return false;
            }

            return true;
        }

        private bool IsPlayerCollider(Collider other) => IsPlayerCollider(other.transform);

        private bool IsPlayerCollider(Transform target) => target.CompareTag(_playerTag);

        private bool IsOwnerTransform(Transform target) =>
            target == _ownerRoot || target.IsChildOf(_ownerRoot);
    }
}
