using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
	class SequentialBrain : IBrain
	{
		private ILayer[] layers;

		public SequentialBrain(ILayer[] layers)
		{
			this.layers = layers;
		}

		public void Process(double[] input)
		{
			layers[0] = new InputLayer(input);
			foreach (var l in layers)
			{
				l.Process();
			}
		}

		public ILayer[] Layers
		{
			get => layers;
		}

		public int InputLength
		{
			get => layers.First().LengthOfData;
		}

		public double[] Output
		{ 
			get => layers.Last().Data; 
		}

		public int OutputLength
		{
			get => layers.Last().LengthOfData;
		}
	}
}
