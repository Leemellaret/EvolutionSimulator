using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Organs
{
	class MoveOrgan : IOrgan
	{
		private int[] dataAddressees;
		public Body Location { get; private set; }


		public void MakeInteraction()
		{
			//return 0;
		}

		public int[] DataAddressees { get => dataAddressees; }
		public int DataAddresseesLength { get => dataAddressees.Length; }
	}
}
