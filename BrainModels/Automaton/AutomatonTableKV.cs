using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.Automaton
{
	public class AutomatonTableKV
	{
		private AutomatonIO ioSymbols;
		private int state;

		public AutomatonTableKV(int[] symbols, int state)
		{
			this.ioSymbols = new AutomatonIO(symbols);
			this.state = state;
		}
		public AutomatonTableKV(AutomatonIO input, int state)
		{
			this.ioSymbols = input;
			this.state = state;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
				return true;

			if (obj == null || !this.GetType().Equals(obj.GetType()))
				return false;

			AutomatonTableKV cObj = (AutomatonTableKV)obj;
			return (ioSymbols.Equals(cObj.ioSymbols) && cObj.state == this.state);
		}
		public override string ToString()
		{
			return $"Symbols=[{ioSymbols.ToString()}] State={state}";
		}
		public override int GetHashCode()
		{
			return Hash.GetHashCodeForATKV(ioSymbols.Symbols, state);
		}

		public int this[int index]
		{
			get
			{
				return ioSymbols[index];
			}
		}
		public AutomatonIO IOSymbols
		{
			get
			{
				return ioSymbols;
			}
		}
		public int Length
		{
			get
			{
				return ioSymbols.Length;
			}
		}
		public int State
		{
			get => state;
		}
	}
}
