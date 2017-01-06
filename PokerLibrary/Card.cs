using System;
using System.Runtime.InteropServices;

namespace PokerLibrary {
	// [StructLayout(LayoutKind.Sequential)]
	// struct is slower than class, [tested]
	public class Card {
		private readonly CardType m_CardType;
		private readonly CardValue m_CardValue;
		private readonly byte m_Series;

		public Card(CardType cardType, CardValue cardValue, byte series) {
			this.m_CardType = cardType;
			this.m_CardValue = cardValue;
			this.m_Series = series;
		}

		public CardType CardType {
			get {
				return this.m_CardType;
			}
		}

		public CardValue CardValue {
			get {
				return this.m_CardValue;
			}
		}

		public byte Series {
			get {
				return this.m_Series;
			}
		}

		public bool IsHeart {
			get {
				return (this.m_CardType == CardType.Hearts);
			}
		}

		public bool IsDiamond {
			get {
				return (this.m_CardType == CardType.Diamonds);
			}
		}

		public bool IsSpade {
			get {
				return (this.m_CardType == CardType.Spades);
			}
		}

		public bool IsClub {
			get {
				return (this.m_CardType == CardType.Clubs);
			}
		}

		public bool IsRed {
			get {
				return (this.m_CardType == CardType.Hearts || this.m_CardType == CardType.Diamonds);
			}
		}

		public bool IsBlack {
			get {
				return (this.m_CardType == CardType.Spades || this.m_CardType == CardType.Clubs);
			}
		}

		public bool IsJoker {
			get {
				return (this.m_CardValue == CardValue.Joker);
			}
		}

		public bool IsFaceCard {
			get {
				return (this.m_CardValue == CardValue.Jack || this.m_CardValue == CardValue.Queen || this.m_CardValue == CardValue.King);
			}
		}

		public bool IsSpotCard {
			get {
				return (this.m_CardValue != CardValue.Joker && this.m_CardValue != CardValue.Jack && this.m_CardValue != CardValue.Queen && this.m_CardValue != CardValue.King);
			}
		}

		public string GetTypeName() {
			return CardInfoAttribute.Get(this.m_CardType).Name;
		}

		public string GetValueName() {
			return CardInfoAttribute.Get(this.m_CardValue).Name;
		}

		public int GetTypePriority() {
			return CardInfoAttribute.Get(this.m_CardType).Priority;
		}

		public int GetValuePriority() {
			return CardInfoAttribute.Get(this.m_CardValue).Priority;
		}

		public override bool Equals(object obj) {
			return (this.GetHashCode() == obj.GetHashCode());
		}

		public static int GetHashCode(CardType cardType, CardValue cardValue, byte series) {
			unchecked {
				int _hash = 17;
				_hash = _hash * 23 + cardType.GetHashCode();
				_hash = _hash * 23 + cardValue.GetHashCode();
				_hash = _hash * 23 + series.GetHashCode();

				return _hash;
			}
		}

		public override int GetHashCode() {
			return Card.GetHashCode(this.m_CardType, this.m_CardValue, this.m_Series);
		}
	}
}
