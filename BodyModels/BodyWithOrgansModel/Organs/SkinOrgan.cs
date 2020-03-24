using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.BodyWithOrgansModel.Organs
{
	class SkinOrgan : IOrgan
	{
		private int[] dataAddressees;
		public BodyWithOrgans Body { get; private set; }


		public void MakeInteraction()
		{
			
		}

		public int[] DataAddressees { get => dataAddressees; }
		public int DataAddresseesLength { get => dataAddressees.Length; }
	}
}
