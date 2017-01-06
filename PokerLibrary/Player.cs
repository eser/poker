using System;
using System.Collections.Generic;

namespace PokerLibrary {
	public class Player : ICardContainer {
		private readonly CardCollection<bool> m_Cards;
		private string m_Name;

		public Player() {
			this.m_Cards = new CardCollection<bool>();
		}

		public CardCollection<bool> Cards {
			get {
				return this.m_Cards;
			}
		}

		public string Name {
			get {
				return this.m_Name;
			}
			set {
				this.m_Name = value;
			}
		}
	}
}
