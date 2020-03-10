using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
	class InputLayer : ILayer
	{
		private double[] inputData;

		public InputLayer(double[] input)
		{
			inputData = input;
		}

		public double[] Data
		{
			get => inputData;
		}

		public int LengthOfData
		{
			get => inputData.Length;
		}

		public void Process() { }
	}
}
