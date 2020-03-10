using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.Utils
{
	public static class ShellsOfArray
	{
		public static T[] ReturnOfArrayFromClass<T>(T[] array)
		{
			//return (T[])array.Clone();
			return array;
		}

		public static T[] InitializationOfArrayInConstructor<T>(T[] array)
		{
			//return (T[])array.Clone();
			return array;
		}
	}
}
