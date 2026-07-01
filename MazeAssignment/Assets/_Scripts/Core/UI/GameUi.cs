using Maze.Common;
using Maze.Utilities;
using TMPro;
using UnityEngine;
using Zenject;

namespace Maze.Core.UI
{
    public class GameUi : MonoBehaviour, IGameUi
    {
        [SerializeField] private GameObject _defeatScreen;
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private TMP_Text _diamondCountText;
        [SerializeField] private TMP_Text _victoryDiamondCountText;

        private IGameOrchestrator _orchestrator;

        [Inject]
        public void Construct(IGameOrchestrator orchestrator) => _orchestrator = orchestrator;

        public void UpdateDiamondCount(int collected, int total) => _diamondCountText.SetText("{0}/{1}", collected, total);

        public void ShowDefeatScreen()
        {
            GameCursor.Unlock();
            _defeatScreen.SetActive(true);
        }

        public void ShowVictoryScreen(int collectedDiamonds, int total)
        {
            GameCursor.Unlock();
            _victoryScreen.SetActive(true);
            _victoryDiamondCountText.text = $"You have collected {collectedDiamonds}/{total} diamonds";
        }

        public void OnRestartClicked() => _orchestrator.HandleRestartRequested();

        public void OnQuitClicked() => _orchestrator.HandleQuitRequested();
    }
}
