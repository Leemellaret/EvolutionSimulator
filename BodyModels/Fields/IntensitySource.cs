using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Fields
{
    class IntensitySource
    {
        public Orientation IntensityOrientation { get; private set; }
        public double IntensityValue { get; private set; }

        public IntensitySource(Orientation intensityOrientation, double intensityValue)
        {
            IntensityOrientation = intensityOrientation;
            IntensityValue = intensityValue;
        }
    }
}
