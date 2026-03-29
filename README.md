# Random Deck

Randomness without replacement. Draw from a shuffled deck instead
of rolling dice to reduce frustrating streaks.

Inspired by
[Randomness without Replacement](https://gamedev.net/tutorials/programming/math-and-physics/randomness-without-replacement-r2206/).

## Purpose

When designing games, random events often produce frustrating
streaks. Ten misses in a row with a 50% hit chance is unlikely
but inevitable given enough players. Drawing cards from a deck
instead of rolling dice guarantees that after enough draws,
every outcome appears. This package provides a generic deck
that shuffles and draws without replacement.

## Usage

```csharp
using EthanKennerly.RandomDeck;

var random = new SystemRandom();
var cards = new int[] { 1, 2, 3, 4, 5 };
var deck = new Deck<int>(random, cards);

deck.Shuffle();
int card = deck.Draw();
```

### Prevent Recent Repetition

Set `MinDrawsBetweenRepetition` to prevent the same card from
appearing too soon after it was last drawn. Default is 0, which
behaves like a standard deck. Setting it to 1 prevents the same
card from appearing on consecutive draws across reshuffles.

```csharp
deck.MinDrawsBetweenRepetition = 1;
```

## Installation

Install via the Unity Package Manager:

1. Open your project in Unity.
2. Open the Package Manager (Window > Package Manager).
3. Click the + button and select "Add package from git URL..."
4. Enter: `https://github.com/ethankennerly/random-deck-unity.git`

## Compatibility

- Unity 6.3 and later
- AOT platforms (iOS, WebGL)
- No reflection, no LINQ
