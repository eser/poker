using System;
using System.Collections.Generic;
using PokerLibrary;

namespace PokerServer {
	public class Game {
		private readonly CardCollection<ICardContainer> m_Deck;
		private readonly CardCollection<bool> m_FlopTurnRiver;
		private readonly Dictionary<Guid, Player> m_Players;
		private readonly Ground m_Ground;

		private bool m_IsPlaying;
		private int m_Round;
		private DateTime m_StartTime;

		private int m_BigBlindClient;
		private int m_SmallBlindClient;
		private int m_PlayerTurn;

		public Game() {
			this.m_Deck = new CardCollection<ICardContainer>();
			this.m_FlopTurnRiver = new CardCollection<bool>();
			this.m_Players = new Dictionary<Guid, Player>();
			this.m_Ground = new Ground();

			this.m_Round = 0;
			this.m_IsPlaying = false;
		}

		public CardCollection<ICardContainer> Deck {
			get {
				return this.m_Deck;
			}
		}

		public CardCollection<bool> FlopTurnRiver {
			get {
				return this.m_FlopTurnRiver;
			}
		}

		public Dictionary<Guid, Player> Players {
			get {
				return this.m_Players;
			}
		}

		public Ground Ground {
			get {
				return this.m_Ground;
			}
		}

		public int Round {
			get {
				return this.m_Round;
			}
		}

		public DateTime StartTime {
			get {
				return this.m_StartTime;
			}
		}

		public void CheckGame() {
			if(this.m_IsPlaying) {
				return;
			}

			int _playerCount = this.m_Players.Count;
			if(_playerCount < 1) {
				return;
			}

			Server.Instance.Broadcast("START");

			this.NewRound();
		}

		public void NewRound() {
			this.m_IsPlaying = true;
			this.m_Round++;
			this.m_StartTime = DateTime.UtcNow;

			int _playerCount = this.m_Players.Count;
			this.m_PlayerTurn = (_playerCount + this.m_Round - 2);
			this.m_SmallBlindClient = (this.m_PlayerTurn % _playerCount);
			this.m_PlayerTurn++;
			this.m_BigBlindClient = (this.m_PlayerTurn % _playerCount);
			this.m_PlayerTurn++;

			// Console.WriteLine("Big Blind: {0}", this.m_BigBlindClient);
			// Console.WriteLine("Small Blind: {0}", this.m_SmallBlindClient);

			this.m_Deck.Clear();
			this.CreateCards();
			this.m_Deck.Shuffle();

			//for(int i = 0; i < _playerCount; i++) {
			//    Player _player = this.m_Players[this.m_PlayerTurn % _playerCount];
			//! unordered
			foreach(KeyValuePair<Guid, Player> _pair in this.m_Players) {
				this.GetCardFromDeck(_pair.Value, 2);

				Server.Instance.Clients[_pair.Key].SendEncrypted("CARDS " + _pair.Value.Cards.Serialize());

				this.m_PlayerTurn++;
			}

			// Console.WriteLine("Turn: {0}", this.m_PlayerTurn % _playerCount);
			// Console.WriteLine("--");
		}

		public void NextPlayer() {
			int _playerCount = this.m_Players.Count;

			if((this.m_PlayerTurn % _playerCount) == this.m_BigBlindClient) {
				this.NextStage();
			}

			this.m_PlayerTurn++;
		}

		public void NextStage() {
			if(this.m_Ground.Stage == GroundStage.River) {
				return;
			}

			switch(this.m_Ground.Stage) {
			case GroundStage.None:
				this.GetCardFromDeck(this.m_Ground, 3);
				this.m_Ground.Stage = GroundStage.Flop;
				Console.WriteLine("Flop");

				break;
			case GroundStage.Flop:
				this.GetCardFromDeck(this.m_Ground, 1);
				this.m_Ground.Stage = GroundStage.Turn;
				Console.WriteLine("Turn");

				break;
			case GroundStage.Turn:
				this.GetCardFromDeck(this.m_Ground, 1);
				this.m_Ground.Stage = GroundStage.River;
				Console.WriteLine("River");

				break;
			}

			Server.Instance.Broadcast("GROUND " + this.m_Ground.Stage.ToString() + " " + this.m_Ground.Cards.Serialize());
		}

		public void CreateCards() {
			for(byte _series = 1; _series <= 1; _series++) {
				foreach(CardType _cardType in Enum.GetValues(typeof(CardType))) {
					if(!CardInfoAttribute.Get(_cardType).InDeck) {
						continue;
					}

					foreach(CardValue _cardValue in Enum.GetValues(typeof(CardValue))) {
						if(!CardInfoAttribute.Get(_cardValue).InDeck) {
							continue;
						}

						this.m_Deck.Add(new Card(_cardType, _cardValue, _series), null);
					}
				}
			}
		}

		public void GetCardFromDeck(ICardContainer cardContainer, int count = 1) {
			int i = 0;
			for(int j = 0; j < this.m_Deck.Count; j++) {
				if(this.m_Deck[j].Value != null) {
					continue;
				}

				Card _card = this.m_Deck[j].Key;
				this.m_Deck[_card] = cardContainer;
				cardContainer.Cards.Add(_card, true);

				if(++i >= count) {
					break;
				}
			}
		}

		public void PassCard(ICardContainer cardContainer, Card card) {
			ICardContainer _old = this.m_Deck[card];
			if(_old != null) {
				_old.Cards.Remove(card);
			}

			this.m_Deck[card] = cardContainer;
			cardContainer.Cards.Add(card, true);
		}

		public Card[] GetSortedHand(Player player) {
			List<Card> _cards = new List<Card>();

			foreach(Card _card in player.Cards.Keys) {
				_cards.Add(_card);
			}

			foreach(Card _card in this.m_Ground.Cards.Keys) {
				_cards.Add(_card);
			}

			Card[] _sortedCards = _cards.ToArray();
			for(int i = 0; i < _sortedCards.Length - 1; i++) {
				for(int j = i; j < _sortedCards.Length; j++) {
					int _iPriority = _sortedCards[i].GetValuePriority();
					int _jPriority = _sortedCards[j].GetValuePriority();

					if(_iPriority == _jPriority) {
						_iPriority = _sortedCards[i].GetTypePriority();
						_jPriority = _sortedCards[j].GetTypePriority();
					}

					if(_iPriority < _jPriority) {
						Card _temp = _sortedCards[i];
						_sortedCards[i] = _sortedCards[j];
						_sortedCards[j] = _temp;
					}
				}
			}

			return _sortedCards;
		}

		public int GetPlayerScore(Player player) {
			Card[] _sortedCards = this.GetSortedHand(player);
			int _score = 0;

			for(int i = 0; i < _sortedCards.Length; i++) {
				_score += _sortedCards[i].GetValuePriority();
			}

			return _score;
		}
	}
}

