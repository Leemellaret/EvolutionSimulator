using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BodyModels;

namespace EvolutionSimulator.BodyModels.BodyWithOrgansModel.Organs
{
	class FieldSensorOrgan : IOrgan
	{
		private int[] dataAddressees;
		//public Field Field { get; private set; }
		public BodyWithOrgans Body { get; private set; }


		public void MakeInteraction()
		{
			//принимает воздействия из среды и записыват в тело результат
		}

		public int[] DataAddressees { get => dataAddressees; }
		public int DataAddresseesLength { get => dataAddressees.Length; }
	}
}
