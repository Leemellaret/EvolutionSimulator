using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.Automaton
{
	public class AutomatonIO
	{
		private int[] symbols;

		public AutomatonIO(int[] symbols)
		{
			this.symbols = symbols;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
				return true;

			if (obj == null || !this.GetType().Equals(obj.GetType()))
				return false;

			AutomatonIO cObj = (AutomatonIO)obj;
			return (symbols.SequenceEqual(cObj.symbols));
		}
		public override string ToString()
		{
			return $"Symbols=[{string.Join(",", symbols)}]";
		}
		public override int GetHashCode()
		{
			return Hash.GetHashCode(symbols);
		}

		public int this[int index]
		{
			get
			{
				return symbols[index];
			}
		}
		public int[] Symbols
		{
			get
			{
				return symbols;
			}
		}
		public int Length
		{
			get
			{
				return symbols.Length;
			}
		}
	}
}
