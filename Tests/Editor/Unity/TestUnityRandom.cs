using NUnit.Framework;

namespace EthanKennerly.RandomDeck.Tests
{
    public sealed class TestUnityRandom
    {
        [Test]
        public void GetFloat_ReturnsBetweenZeroAndOne()
        {
            var random = new UnityRandom();
            float value = random.GetFloat();
            Assert.GreaterOrEqual(value, 0f);
            Assert.LessOrEqual(value, 1f);
        }

        [Test]
        public void SetSeed_SameSequence()
        {
            var random = new UnityRandom();
            random.SetSeed(42);
            float first = random.GetFloat();
            random.SetSeed(42);
            float second = random.GetFloat();
            Assert.AreEqual(first, second);
        }

        [Test]
        public void GetFloat_TwoCallsDifferentValues()
        {
            var random = new UnityRandom();
            random.SetSeed(7);
            float first = random.GetFloat();
            float second = random.GetFloat();
            Assert.AreNotEqual(first, second);
        }

        [Test]
        public void DeckWithUnityRandom_DrawReturnsCard()
        {
            var random = new UnityRandom();
            random.SetSeed(99);
            var deck = new Deck<int>(
                random, new int[] { 10, 20, 30 }
            );
            deck.Shuffle();
            int card = deck.Draw();
            Assert.That(
                card == 10 || card == 20 || card == 30,
                "Draw should return a card from the deck"
            );
        }
    }
}
