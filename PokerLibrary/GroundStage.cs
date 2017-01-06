using System;

namespace PokerLibrary {
	public enum GroundStage : byte {
		[CardInfo(Name = "")]
		None,

		[CardInfo(Name = "Flop")]
		Flop,

		[CardInfo(Name = "Turn")]
		Turn,

		[CardInfo(Name = "River")]
		River
	}
}

