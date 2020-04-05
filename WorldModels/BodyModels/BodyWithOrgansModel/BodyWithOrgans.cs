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
		public string Id { get; }
		public IWorld World { get; private set; }
		public ICreature Creature { get; private set; }
		public double Energy { get; internal set; }

		internal double[] data;
		public BodyOrientation Orientation { get; private set; }
		private List<IOrgan> organs;

		public void InteractWithWorld(double[] commands)
		{
			foreach(IOrgan organ in organs)
			{
				organ.MakeInteraction();
			}
		}

		public void Die()
		{

		}

		public List<IOrgan> Organs { get => organs; }

		public double[] Data { get => data; }
	}
}
