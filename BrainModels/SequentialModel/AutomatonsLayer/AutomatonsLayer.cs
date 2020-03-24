using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.Utils;

namespace EvolutionSimulator.BrainModels.SequentialModel.AutomatonsLayer
{
	class AutomatonsLayer : ILayer
	{
		private AutomatonInLayer[] automatons;
		private double[] processedData;

		public AutomatonsLayer(AutomatonInLayer[] automatons)
		{
			this.automatons = automatons;

			int processedDataLength = 0;
			for (int i = 0; i < automatons.Length; i++)
			{
				processedDataLength += automatons[i].Automaton.OutputLength;
			}

			processedData = new double[processedDataLength];
		}

		public double[] Data
		{
			get => processedData;
		}

		public uint LengthOfData
		{
			get => (uint)processedData.Length;
		}

		public void Process()
		{
			int putIndex = 0;
			double[] aOutput;

			for (int i = 0; i < automatons.Length; i++)
			{
				aOutput = automatons[i].Process();
				for (int j = 0; j < aOutput.Length; j++, putIndex++)
				{
					processedData[putIndex] = aOutput[j];
				}
			}
		}
	}
}
