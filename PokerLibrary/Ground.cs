using System;

namespace PokerLibrary {
	public class Ground : ICardContainer {
		private readonly CardCollection<bool> m_Cards;
		private readonly string m_Name;
		private GroundStage m_Stage;

		public Ground() {
			this.m_Cards = new CardCollection<bool>();
			this.m_Name = "Ground";
			this.m_Stage = GroundStage.None;
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
		}

		public GroundStage Stage {
			get {
				return this.m_Stage;
			}
			set {
				this.m_Stage = value;
			}
		}
	}
}

