using System;
using System.Reflection;

namespace PokerLibrary {
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class CardInfoAttribute : Attribute {
		private string m_Name;
		private bool m_InDeck;
		private int m_Priority;

		public string Name {
			get {
				return this.m_Name;
			}
			set {
				this.m_Name = value;
			}
		}

		public bool InDeck {
			get {
				return this.m_InDeck;
			}
			set {
				this.m_InDeck = value;
			}
		}

		public int Priority {
			get {
				return this.m_Priority;
			}
			set {
				this.m_Priority = value;
			}
		}

		public CardInfoAttribute() : base() {
		}

		public static CardInfoAttribute Get(object obj) {
			Type _type = obj.GetType();

			foreach(FieldInfo _field in _type.GetFields()) {
				CardInfoAttribute _attribute = Attribute.GetCustomAttribute(_field, typeof(CardInfoAttribute)) as CardInfoAttribute;

				if(_attribute == null) {
					continue;
				}

				if(!obj.Equals(_field.GetValue(null))) {
					continue;
				}

				return _attribute;
			}

			return null;
		}
	}
}
