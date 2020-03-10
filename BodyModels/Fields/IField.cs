using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Fields
{
    interface IField
    {
        string FieldName { get; }
        double GetIntensityFromPosition(Orientation orientation);
        void CreateIntensityIn(Orientation orientation);
        void RemoveIntensityIn(Orientation orientation);
        void ChangeIntensityIn(Orientation orientation);
        void ClearField();
    }
}
