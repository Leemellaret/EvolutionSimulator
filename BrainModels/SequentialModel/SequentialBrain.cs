using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.CreatureModels;

namespace EvolutionSimulator.BrainModels.SequentialModel
{
	class SequentialBrain : IBrain
	{
		public ICreature Creature { get; private set; }
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

		public uint InputLength
		{
			get => layers.First().LengthOfData;
		}

		public double[] Commands
		{ 
			get => layers.Last().Data; 
		}

		public double GetCommand(uint index)
		{
			return Commands[index];
		}

		public uint CommandsLength
		{
			get => layers.Last().LengthOfData;
		}
	}
}
