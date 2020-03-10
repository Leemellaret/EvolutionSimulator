using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Fields
{
    class FoodField : IField
    {
        List<IntensitySource> intensitySources;
        public string FieldName { get; private set; }

        public FoodField()
        {
            intensitySources = new List<IntensitySource>();
            FieldName = "food";
        }
        public double GetIntensityFromPosition(Orientation orientation)//получить интенсивность взаимодействия с позиции
        {

        }
        public void CreateIntensityIn(Orientation orientation)
        {

        }
        public void RemoveIntensityIn(Orientation orientation)
        {

        }
        public void ChangeIntensityIn(Orientation orientation)
        {
            var src = intensitySources.Find(x => x.IntensityOrientation.Equals(orientation));
            
        }
        public void ClearField()
        {

        }
    }
}
