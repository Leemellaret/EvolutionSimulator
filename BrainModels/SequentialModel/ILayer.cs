using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
	public interface ILayer
	{
		double[] Data { get; }
		int LengthOfData { get; }
		void Process();
	}
}
