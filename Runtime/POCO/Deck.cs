using System.Collections.Generic;

namespace EthanKennerly.RandomDeck
{
    public sealed class Deck<T>
    {
        private readonly IRandom _random;
        private readonly T[] _cards;
        private int _remaining;
        private int _minDrawsBetweenRepetition;
        private T _lastDrawn;
        private bool _hasLastDrawn;

        public int MinDrawsBetweenRepetition
        {
            get { return _minDrawsBetweenRepetition; }
            set { _minDrawsBetweenRepetition = value; }
        }

        public Deck(IRandom random, IEnumerable<T> cards)
        {
            _random = random;
            var list = new List<T>();
            foreach (T card in cards)
            {
                list.Add(card);
            }
            _cards = list.ToArray();
            _remaining = _cards.Length;
        }

        public void Shuffle()
        {
            _remaining = _cards.Length;
            FisherYatesShuffle(0, _remaining);
            if (_minDrawsBetweenRepetition >= 1
                && _hasLastDrawn
                && _remaining > 1)
            {
                MoveLastDrawnAwayFromTop();
            }
        }

        public T Draw()
        {
            if (_remaining <= 0)
            {
                Shuffle();
            }
            _remaining--;
            T drawn = _cards[_remaining];
            _lastDrawn = drawn;
            _hasLastDrawn = true;
            return drawn;
        }

        private void FisherYatesShuffle(
            int startInclusive,
            int endExclusive)
        {
            for (int i = endExclusive - 1;
                i > startInclusive;
                i--)
            {
                int j = (int)(
                    _random.GetFloat() * (i - startInclusive + 1)
                ) + startInclusive;
                if (j >= endExclusive)
                {
                    j = endExclusive - 1;
                }
                T temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }

        private void MoveLastDrawnAwayFromTop()
        {
            int topIndex = _remaining - 1;
            if (!EqualityComparer<T>.Default.Equals(
                _cards[topIndex], _lastDrawn))
            {
                return;
            }
            int swapIndex = (int)(
                _random.GetFloat() * (_remaining - 1)
            );
            T temp = _cards[topIndex];
            _cards[topIndex] = _cards[swapIndex];
            _cards[swapIndex] = temp;
        }
    }
}
