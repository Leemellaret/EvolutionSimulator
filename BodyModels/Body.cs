using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BodyModels.Organs;

namespace EvolutionSimulator.BodyModels
{
	class Body : IBody
	{
		private double energy;
		private double[] data;
		private Orientation orientation;
		private List<IOrgan>[] sidesWithOrgans;
	}
}
