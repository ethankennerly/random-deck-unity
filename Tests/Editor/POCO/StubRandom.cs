namespace EthanKennerly.RandomDeck.Tests
{
    public sealed class StubRandom : IRandom
    {
        private float[] _floats;
        private int _index;

        public StubRandom(params float[] floats)
        {
            _floats = floats;
            _index = 0;
        }

        public void SetSeed(int seed)
        {
            _index = 0;
        }

        public float GetFloat()
        {
            if (_floats == null || _floats.Length == 0)
            {
                return 0f;
            }
            float value = _floats[_index % _floats.Length];
            _index++;
            return value;
        }
    }
}
