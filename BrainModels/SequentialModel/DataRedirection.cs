using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
	class DataRedirection
	{
		public ILayer Layer { get; private set; }
		public int IndexOfData { get; private set; }

		public DataRedirection(ILayer layer, int indexOfData)
		{
			Layer = layer;
			IndexOfData = indexOfData;
		}
	}
}
