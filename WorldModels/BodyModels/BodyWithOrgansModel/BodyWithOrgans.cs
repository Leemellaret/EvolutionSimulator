using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels.BodyModels.BodyWithOrgansModel.Organs;
using EvolutionSimulator.CreatureModels;
using EvolutionSimulator.API;

namespace EvolutionSimulator.WorldModels.BodyModels.BodyWithOrgansModel
{
	public class BodyWithOrgans : IBody
	{
		public IWorld World { get; private set; }
		public ICreature Creature { get; private set; }
		public double Energy { get; internal set; }

		internal double[] data;
		public Orientation Orientation { get; private set; }
		private List<IOrgan> organs;

		public void InteractWithWorld()
		{
			foreach(IOrgan organ in organs)
			{
				organ.MakeInteraction();
			}
		}

		public List<IOrgan> Organs { get => organs; }

		public double[] Data { get => data; }
	}
}
