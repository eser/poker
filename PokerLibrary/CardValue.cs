using System;

namespace PokerLibrary {
	public enum CardValue : byte {
		[CardInfo(Name = "A", InDeck = false, Priority = 1)]
		LowAce,

		[CardInfo(Name = "2", InDeck = true, Priority = 2)]
		Two,

		[CardInfo(Name = "3", InDeck = true, Priority = 3)]
		Three,

		[CardInfo(Name = "4", InDeck = true, Priority = 4)]
		Four,

		[CardInfo(Name = "5", InDeck = true, Priority = 5)]
		Five,

		[CardInfo(Name = "6", InDeck = true, Priority = 6)]
		Six,

		[CardInfo(Name = "7", InDeck = true, Priority = 7)]
		Seven,

		[CardInfo(Name = "8", InDeck = true, Priority = 8)]
		Eight,

		[CardInfo(Name = "9", InDeck = true, Priority = 9)]
		Nine,

		[CardInfo(Name = "10", InDeck = true, Priority = 10)]
		Ten,

		[CardInfo(Name = "J", InDeck = true, Priority = 11)]
		Jack,

		[CardInfo(Name = "Q", InDeck = true, Priority = 12)]
		Queen,

		[CardInfo(Name = "K", InDeck = true, Priority = 13)]
		King,

		[CardInfo(Name = "A", InDeck = true, Priority = 14)]
		Ace,

		[CardInfo(Name = "*", InDeck = false, Priority = 15)]
		Joker
	}
}
