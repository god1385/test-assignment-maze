namespace Maze.Core.Diamonds
{
    public class DiamondCollection : IDiamondCollection
    {
        private int _totalCount;
        private int _collectedCount;

        public int CollectedCount => _collectedCount;

        public int TotalCount => _totalCount;

        public bool AllCollected => _totalCount > 0 && _collectedCount >= _totalCount;

        public void SetTotal(int total)
        {
            _totalCount = total;
            _collectedCount = 0;
        }

        public void RegisterCollected()
        {
            if (_collectedCount < _totalCount)
                _collectedCount++;
        }
    }
}
