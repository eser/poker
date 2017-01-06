using System;
using System.Collections.Generic;
using System.Text;

namespace PokerLibrary {
	public class CardCollection<T> {
		private List<Card> m_Keys;
		private List<T> m_Values;

		public CardCollection() {
			this.m_Keys = new List<Card>();
			this.m_Values = new List<T>();
		}

		public T this[Card card] {
			get {
				return this.m_Values[this.m_Keys.IndexOf(card)];
			}
			set {
				this.m_Values[this.m_Keys.IndexOf(card)] = value;
			}
		}

		public KeyValuePair<Card, T> this[int index] {
			get {
				return new KeyValuePair<Card, T>(this.m_Keys[index], this.m_Values[index]);
			}
			set {
				this.m_Keys[index] = value.Key;
				this.m_Values[index] = value.Value;
			}
		}

		public List<Card> Keys {
			get {
				return this.m_Keys;
			}
		}

		public int Count {
			get {
				return this.m_Keys.Count;
			}
		}

		public void Add(Card card, T value) {
			this.m_Keys.Add(card);
			this.m_Values.Add(value);
		}

		public void Remove(Card card) {
			this.m_Keys.Remove(card);
		}

		public void Clear() {
			this.m_Keys.Clear();
			this.m_Values.Clear();
		}

		public bool Contains(Card card) {
			return this.m_Keys.Contains(card);
		}

		public int IndexOf(CardType cardType, CardValue cardValue) {
			for(int i = 0; i < this.m_Keys.Count; i++) {
				if(this.m_Keys[i].CardType == cardType && this.m_Keys[i].CardValue == cardValue) {
					return i;
				}
			}

			return -1;
		}

		public int IndexOf(CardType cardType, CardValue cardValue, int series) {
			for(int i = 0; i < this.m_Keys.Count; i++) {
				if(this.m_Keys[i].CardType == cardType && this.m_Keys[i].CardValue == cardValue && this.m_Keys[i].Series == series) {
					return i;
				}
			}

			return -1;
		}

		public void Shuffle() {
			List<Card> _oldCardKeys = new List<Card>(this.m_Keys);
			List<T> _oldCardValues = new List<T>(this.m_Values);

			this.m_Keys.Clear();
			this.m_Values.Clear();

			Random _rand = new Random();
			while(_oldCardKeys.Count > 0) {
				int _key = _rand.Next(0, _oldCardKeys.Count - 1);

				this.m_Keys.Add(_oldCardKeys[_key]);
				this.m_Values.Add(_oldCardValues[_key]);

				_oldCardKeys.RemoveAt(_key);
				_oldCardValues.RemoveAt(_key);
			}
		}

		public string Serialize() {
			if(this.m_Keys.Count <= 0) {
				return string.Empty;
			}

			StringBuilder _str = new StringBuilder();

			for(int i = 0;i < this.m_Keys.Count;i++) {
				Card _card = this.m_Keys[i];
				_str.Append(_card.CardType.ToString());
				_str.Append("|");
				_str.Append(_card.CardValue);
				_str.Append("|");
				_str.Append(_card.Series);
				_str.AppendLine();
			}

			return _str.ToString(0, _str.Length - 1);
		}
	}
}

