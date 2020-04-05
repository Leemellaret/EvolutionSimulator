using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels.Fields;
using EvolutionSimulator.API;

namespace EvolutionSimulator.WorldModels.BodyModels.BodyWithOrgansModel.Organs
{
	class FoodAbsorbingOrgan : IOrgan
	{
        private uint[] commandsIndexes;
		public double MaxAbsorbingValue { get; private set; }
        
        public Range DataWriteRange { get; private set; }//[begin] - Сколько еды было съедено
		public FoodField Field { get; private set; }
		public IBody Body { get; private set; }
		public WorldDirection BodySide { get; private set; }
		public uint[] CommandsIndexes { get => commandsIndexes; }//[0] - =<0 не есть, >0 - есть на это значение

		BodyWithOrgans body;

		public FoodAbsorbingOrgan(
			uint[] commandsIndexes, 
			double maxAbsorbingValue, 
			Range dataWriteRange, 
			FoodField field, 
			BodyWithOrgans body, 
			WorldDirection bodySide)
		{
			this.commandsIndexes = (uint[])commandsIndexes;
			MaxAbsorbingValue = maxAbsorbingValue;
			DataWriteRange = dataWriteRange;
			Body = body;
			this.body = body;
			BodySide = bodySide;
		}

		public void MakeInteraction()
		{
			double absorbVal = 0;// Body.Creature.Brain.GetCommand(0);
			if (absorbVal > 0)
			{
				var orientRelToThis = new BodyOrientation(Body.Orientation.X, Body.Orientation.Y, BodySide);
				var dir = orientRelToThis.RotateDirection((int)Body.Orientation.Direction);

				double intensInCell = Field.GetIntensityIn(dir);

				if (intensInCell <= absorbVal)
				{
					body.Energy += intensInCell;
					Field.RemoveIntensityIn(dir);
					body.data[DataWriteRange.Begin] = intensInCell;
				}
				else
				{
					body.Energy += absorbVal;
					Field.ChangeIntensityIn(dir, -absorbVal);
					body.data[DataWriteRange.Begin] = absorbVal;
				}
			}
		}


	}
}
