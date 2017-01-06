using System;

namespace PokerLibrary {
	public enum CardType : byte {
		[CardInfo(Name = "", InDeck = false, Priority = 0)]
		None,

		[CardInfo(Name = "Diamond", InDeck = true, Priority = 1)]
		Diamonds,

		[CardInfo(Name = "Heart", InDeck = true, Priority = 2)]
		Hearts,

		[CardInfo(Name = "Spade", InDeck = true, Priority = 3)]
		Spades,

		[CardInfo(Name = "Club", InDeck = true, Priority = 4)]
		Clubs
	}
}
