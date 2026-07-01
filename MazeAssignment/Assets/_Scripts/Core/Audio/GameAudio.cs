using Maze.Common;
using UnityEngine;

namespace Maze.Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameAudio : MonoBehaviour, IGameAudio
    {
        [SerializeField] private AudioClip _enemyDetectClip;
        [SerializeField] private AudioClip _enemyCatchClip;
        [SerializeField] private AudioClip _diamondCollectClip;
        [SerializeField] private AudioClip _victoryClip;
        [SerializeField] private AudioSource _audioSource;

        public void PlayEnemyDetect() => PlayClip(_enemyDetectClip);

        public void PlayEnemyCatch() => PlayClip(_enemyCatchClip);

        public void PlayDiamondCollect() => PlayClip(_diamondCollectClip);

        public void PlayVictory() => PlayClip(_victoryClip);

        private void Awake()
        {
            _audioSource.spatialBlend = 0f;
            _audioSource.playOnAwake = false;
        }

        private void Reset() => _audioSource = GetComponent<AudioSource>();

        private void PlayClip(AudioClip clip) => _audioSource.PlayOneShot(clip, 1f);
    }
}
