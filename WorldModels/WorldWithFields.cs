using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BodyModels.Fields;
using EvolutionSimulator.BodyModels;

namespace EvolutionSimulator.WorldModels
{
    class WorldWithFields : IWorld
    {
        public List<IField> Fields { get; private set; }
        public List<IBody> Bodies { get; private set; }

    }
}
