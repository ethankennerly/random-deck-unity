using NUnit.Framework;

namespace EthanKennerly.RandomDeck.Tests
{
    public sealed class TestDeck
    {
        [Test]
        public void Draw_ReturnsCardFromDeck()
        {
            var random = new StubRandom(0f);
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

        [Test]
        public void Draw_AllCards_ReturnsEachCardOnce()
        {
            var random = new StubRandom(0f);
            var deck = new Deck<int>(
                random, new int[] { 1, 2, 3 }
            );
            deck.Shuffle();
            bool[] seen = new bool[4];
            for (int i = 0; i < 3; i++)
            {
                int card = deck.Draw();
                Assert.IsFalse(
                    seen[card],
                    "Each card drawn once before reshuffle"
                );
                seen[card] = true;
            }
            Assert.IsTrue(seen[1]);
            Assert.IsTrue(seen[2]);
            Assert.IsTrue(seen[3]);
        }

        [Test]
        public void Draw_ExhaustsDeck_ReshufflesAutomatically()
        {
            var random = new StubRandom(0.5f);
            var deck = new Deck<int>(
                random, new int[] { 1, 2 }
            );
            deck.Shuffle();
            deck.Draw();
            deck.Draw();
            int card = deck.Draw();
            Assert.That(
                card == 1 || card == 2,
                "After exhaustion, reshuffle returns a card"
            );
        }

        [Test]
        public void Shuffle_RandomizesOrder()
        {
            var random = new SystemRandom();
            random.SetSeed(123);
            int[] cards = new int[20];
            for (int i = 0; i < 20; i++)
            {
                cards[i] = i;
            }
            var deck = new Deck<int>(random, cards);
            deck.Shuffle();
            int[] drawn = new int[20];
            for (int i = 0; i < 20; i++)
            {
                drawn[i] = deck.Draw();
            }
            bool allSame = true;
            for (int i = 0; i < 20; i++)
            {
                if (drawn[i] != i)
                {
                    allSame = false;
                    break;
                }
            }
            Assert.IsFalse(
                allSame,
                "Shuffle should randomize the order"
            );
        }

        [Test]
        public void Shuffle_DeterministicWithStubRandom()
        {
            var random = new StubRandom(0f);
            var deck = new Deck<int>(
                random, new int[] { 1, 2, 3 }
            );
            deck.Shuffle();
            int first = deck.Draw();
            int second = deck.Draw();
            int third = deck.Draw();

            var random2 = new StubRandom(0f);
            var deck2 = new Deck<int>(
                random2, new int[] { 1, 2, 3 }
            );
            deck2.Shuffle();
            Assert.AreEqual(first, deck2.Draw());
            Assert.AreEqual(second, deck2.Draw());
            Assert.AreEqual(third, deck2.Draw());
        }

        [Test]
        public void MinDrawsBetweenRepetition_DefaultZero()
        {
            var random = new StubRandom(0f);
            var deck = new Deck<int>(
                random, new int[] { 1 }
            );
            Assert.AreEqual(0, deck.MinDrawsBetweenRepetition);
        }

        [Test]
        public void MinDrawsBetweenRepetition_One_PreventsConsecutive()
        {
            var random = new SystemRandom();
            random.SetSeed(42);
            var deck = new Deck<int>(
                random, new int[] { 1, 2 }
            );
            deck.MinDrawsBetweenRepetition = 1;
            deck.Shuffle();
            int previous = deck.Draw();
            for (int i = 0; i < 100; i++)
            {
                int current = deck.Draw();
                Assert.AreNotEqual(
                    previous,
                    current,
                    "MinDrawsBetweenRepetition=1 " +
                    "prevents consecutive duplicates"
                );
                previous = current;
            }
        }

        [Test]
        public void Draw_StringCards()
        {
            var random = new StubRandom(0.5f);
            var deck = new Deck<string>(
                random,
                new string[] { "hit", "miss" }
            );
            deck.Shuffle();
            string card = deck.Draw();
            Assert.That(
                card == "hit" || card == "miss",
                "Deck works with string type"
            );
        }
    }
}
