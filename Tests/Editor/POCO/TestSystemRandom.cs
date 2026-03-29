using NUnit.Framework;

namespace EthanKennerly.RandomDeck.Tests
{
    public sealed class TestSystemRandom
    {
        [Test]
        public void GetFloat_ReturnsBetweenZeroAndOne()
        {
            var random = new SystemRandom();
            float value = random.GetFloat();
            Assert.GreaterOrEqual(value, 0f);
            Assert.Less(value, 1f);
        }

        [Test]
        public void SetSeed_SameSequence()
        {
            var random = new SystemRandom();
            random.SetSeed(42);
            float first = random.GetFloat();
            random.SetSeed(42);
            float second = random.GetFloat();
            Assert.AreEqual(first, second);
        }

        [Test]
        public void GetFloat_TwoCallsDifferentValues()
        {
            var random = new SystemRandom();
            random.SetSeed(7);
            float first = random.GetFloat();
            float second = random.GetFloat();
            Assert.AreNotEqual(first, second);
        }
    }
}
