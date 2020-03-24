using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.Automaton
{ // TODO Возможно придется сделать вместо results массив функций, которые будут вычислять выход конвертера
	public class DataConverter<T, TResult>
		where T : IComparable<T>
	{
		private T[] valuesInComparisons;
		private TResult[] results;
		private Tuple<Comparison[], int>[] comparisons;//если аргумент Convert`а удовлетворил всем условиям Item1 (1 -- с значением по какому индексу в valuesInComparisons сравнивать; 2 -- как сравнивать), то вернуть значение из results по индексу item2
		private int defaultResultIndex;

		public DataConverter(T[] valuesInComparisons, TResult[] results, Tuple<Comparison[], int>[] comparisons, int defaultResultIndex)
		{
			this.valuesInComparisons = valuesInComparisons;
			this.results = results;
			this.comparisons = comparisons;
			this.defaultResultIndex = defaultResultIndex;
		}

		public T[] ValuesInComparisons
		{
			get => valuesInComparisons;
			set => valuesInComparisons = value;
		}
		public TResult[] Results
		{
			get => results;
			set => results = value;
		}
		public Tuple<Comparison[], int>[] Comparisons
		{
			get => comparisons;
		}
		public int DefaultResultIndex
		{
			get => defaultResultIndex;
			set => defaultResultIndex = value;
		}
		public TResult DefaultResult
		{
			get => results[defaultResultIndex];
		}

		public TResult Convert(T value)
		{
			for (int i = 0; i < comparisons.Length; i++)
			{
				var currentComparisonList = comparisons[i].Item1;
				bool isSatisfy = true;
				for (int j = 0; j < currentComparisonList.Length; j++)
				{
					var cmp = currentComparisonList[j];
					if (!Compare(value, valuesInComparisons[cmp.IndexOfValue], cmp.Type))
					{
						isSatisfy = false;
						break;
					}
				}
				if (isSatisfy) return results[comparisons[i].Item2];
			}
			return results[defaultResultIndex];
		}

		public override string ToString()
		{
			string res = "";
			foreach (var dis in comparisons)
			{
				res += " [";
				foreach (var con in dis.Item1)
				{
					res += $"(x{Comparison.compNames[(int)con.Type]}{valuesInComparisons[con.IndexOfValue]}) && ";
				}
				res = string.Join("", res.Take(res.Length - 4)) + "]";
				res += $"->[{results[dis.Item2]}] ";
			}
			return res;
		}
		public static bool Compare(T comparedValue, T value, TypeOfComparison type)
		{
			if (type == TypeOfComparison.Equal)
			{
				return comparedValue.Equals(value);
			}
			else if (type == TypeOfComparison.NotEqual)
			{
				return !comparedValue.Equals(value);
			}
			else if (type == TypeOfComparison.Bigger)
			{
				return (comparedValue.CompareTo(value) > 0);
			}
			else if (type == TypeOfComparison.BiggerOrEqual)
			{
				return (comparedValue.CompareTo(value) >= 0);
			}
			else if (type == TypeOfComparison.Less)
			{
				return (comparedValue.CompareTo(value) < 0);
			}
			else
			{
				return (comparedValue.CompareTo(value) <= 0);
			}
		}
	}
	public enum TypeOfComparison
	{
		Equal,
		NotEqual,
		Bigger,
		BiggerOrEqual,
		Less,
		LessOrEqual
	}
	public class Comparison
	{
		public int IndexOfValue { get; set; }
		public TypeOfComparison Type { get; set; }

		public Comparison(TypeOfComparison type, int indexOfValue)
		{
			Type = type;
			IndexOfValue = indexOfValue;
		}

		public static string[] compNames = new string[] { "==", "!=", ">", ">=", "<", "<=" };
		public override string ToString()
		{
			return $"x{compNames[(int)Type]}cv[{IndexOfValue}]";
		}
	}
	public static class DCHelper
	{
		public static Tuple<Comparison[], int> CreateCI(int indexOfValue, TypeOfComparison type, int indexOfResult)
		{
			var c = new Comparison(type, indexOfValue);
			return new Tuple<Comparison[], int>(new Comparison[] { c }, indexOfResult);
		}
		public static Tuple<Comparison[], int> CreateCI(int indexOfValue1, TypeOfComparison type1, int indexOfValue2, TypeOfComparison type2, int indexOfResult)
		{
			var c1 = new Comparison(type1, indexOfValue1);
			var c2 = new Comparison(type2, indexOfValue2);
			return new Tuple<Comparison[], int>(new Comparison[] { c1, c2 }, indexOfResult);
		}
		public static Tuple<Comparison[], int> CreateCI(int indexOfResult, params Comparison[] comparisons)
		{
			return new Tuple<Comparison[], int>(comparisons, indexOfResult);
		}
		public static Tuple<Comparison[], int>[] CreateCIArray(Tuple<Comparison[], int> c1)
		{
			return new Tuple<Comparison[], int>[] { c1 };
		}
		public static Tuple<Comparison[], int>[] CreateCIArray(Tuple<Comparison[], int> c1, Tuple<Comparison[], int> c2)
		{
			return new Tuple<Comparison[], int>[] { c1, c2 };
		}
		public static Tuple<Comparison[], int>[] CreateCIArray(params Tuple<Comparison[], int>[] c)
		{
			return c;
		}
	}
}
