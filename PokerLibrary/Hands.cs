using System;

namespace PokerLibrary {
	public enum Hands : byte {
		[CardInfo(Name = "Flush Royal")]
		FlushRoyal,

		[CardInfo(Name = "Straight Flush")]
		StraightFlush,

		[CardInfo(Name = "Four of a Kind")]
		FourOfAKind,

		[CardInfo(Name = "Full House")]
		FullHouse,

		[CardInfo(Name = "Flush")]
		Flush,

		[CardInfo(Name = "Straight")]
		Straight,

		[CardInfo(Name = "Three of a Kind")]
		ThreeOfAKind,

		[CardInfo(Name = "Two Pairs")]
		TwoPairs,

		[CardInfo(Name = "Pair")]
		Pair,

		[CardInfo(Name = "High Card")]
		HighCard
	}
}
