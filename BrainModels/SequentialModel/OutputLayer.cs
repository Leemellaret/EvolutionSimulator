using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
    class OutputLayer : ILayer
	{
		private DataRedirection[] toOutputRedirections;
		private double[] output;

		public OutputLayer(DataRedirection[] toOutputRedirections)
		{
			this.toOutputRedirections = toOutputRedirections;
			output = new double[toOutputRedirections.Length];
		}

		public double[] Data
		{
			get => output;
		}
		public int LengthOfData
		{
			get => toOutputRedirections.Length;
		}
		public void Process()
		{
			for (int i = 0; i < toOutputRedirections.Length; i++)
			{
				output[i] = toOutputRedirections[i].Layer.Data[toOutputRedirections[i].IndexOfData];
			}
		}
	}
}
