using System;

namespace PokerLibrary {
	public interface ICardContainer {
		CardCollection<bool> Cards {
			get;
		}

		string Name {
			get;
		}
	}
}

