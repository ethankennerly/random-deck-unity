namespace EthanKennerly.RandomDeck
{
    public sealed class SystemRandom : IRandom
    {
        private System.Random _random;

        public SystemRandom()
        {
            _random = new System.Random();
        }

        public void SetSeed(int seed)
        {
            _random = new System.Random(seed);
        }

        public float GetFloat()
        {
            return (float)_random.NextDouble();
        }
    }
}
