using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Fields
{
    class IntensitySource
    {
        public Orientation Orientation { get; private set; }
        public double Value { get; private set; }

        public IntensitySource(Orientation orientation, double value)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (value == 0)
                throw new ArgumentException("Интенсивность взаимодействия не может быть равна 0", "value");

            Orientation = orientation;
            Value = value;
        }
    }
}
