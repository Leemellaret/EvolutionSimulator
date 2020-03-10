using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BodyModels.Fields;

namespace EvolutionSimulator.BodyModels.Organs
{
	class FoodAbsorbingOrgan : IOrgan
	{
		private int[] dataAddressees;
		public FoodField Field { get; private set; }
		public Body Location { get; private set; }


		public void MakeInteraction()
		{
			//принимает воздействия из среды и записыват в тело результат
			//return 0;
		}

		public int[] DataAddressees { get => dataAddressees; }
		public int DataAddresseesLength { get => dataAddressees.Length; }
	}
}
