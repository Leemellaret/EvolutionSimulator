using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.Automaton
{
	public class SymbolType
	{
		private int intervalBegin;
		private int intervalEnd;

		public SymbolType(int intervalBegin, int intervalEnd)
		{
			if (intervalBegin > intervalEnd)
			{
				throw new AutomatonException("error: intervalBegin > intervalEnd");
			}

			this.intervalBegin = intervalBegin;
			this.intervalEnd = intervalEnd;
		}

		public int IntervalBegin { get => intervalBegin; }
		public int IntervalEnd { get => intervalEnd; }
		public int IntervalLength { get => intervalEnd - intervalBegin + 1; }
		public bool Contains(int value)
		{
			return (intervalBegin <= value && value <= intervalEnd);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
				return true;

			if (obj == null || !this.GetType().Equals(obj.GetType()))
				return false;

			SymbolType cObj = (SymbolType)obj;
			return (this.intervalBegin == cObj.intervalBegin && this.intervalEnd == cObj.intervalEnd);
		}
		public override string ToString()
		{
			return string.Format("Begin={0} End={1} Length={2}", intervalBegin, intervalEnd, IntervalLength);
		}
		public override int GetHashCode()
		{
			return Hash.GetHashCode(new int[] { intervalBegin, intervalEnd });
		}
	}

	static class SymbolTypeUtils
	{
		public static bool IsComplyWith(int[] array, SymbolType[] symbolTypes)
		{
			if (array.Length != symbolTypes.Length) return false;

			for (int i = 0; i < array.Length; i++)
			{
				if (!symbolTypes[i].Contains(array[i])) return false;
			}

			return true;
		}
	}
}
