namespace EthanKennerly.RandomDeck
{
    public sealed class UnityRandom : IRandom
    {
        public void SetSeed(int seed)
        {
            UnityEngine.Random.InitState(seed);
        }

        public float GetFloat()
        {
            return UnityEngine.Random.value;
        }
    }
}
