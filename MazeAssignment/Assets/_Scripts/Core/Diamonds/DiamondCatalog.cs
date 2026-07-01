using Maze.Common;
using UnityEngine;
using Zenject;

namespace Maze.Core.Diamonds
{
    public class DiamondCatalog : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject[] _diamonds;

        private IDiamondCollection _collection;
        private IGameUi _gameUi;

        [Inject]
        public void Construct(IDiamondCollection collection, IGameUi gameUi)
        {
            _collection = collection;
            _gameUi = gameUi;
        }

        public void Initialize()
        {
            _collection.SetTotal(CountDiamonds());
            _gameUi.UpdateDiamondCount(_collection.CollectedCount, _collection.TotalCount);
        }

        private int CountDiamonds()
        {
            var count = 0;

            foreach (var diamond in _diamonds)
            {
                if (diamond != null)
                    count++;
            }

            return count;
        }
    }
}
