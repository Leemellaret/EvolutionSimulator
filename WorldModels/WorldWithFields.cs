using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels.Fields;
using EvolutionSimulator.WorldModels.BodyModels;
using EvolutionSimulator.API;

namespace EvolutionSimulator.WorldModels
{
    class WorldWithFields : IWorld
    {
        public List<IField> Fields { get; private set; }
        public List<IBody> Bodies { get; private set; }

        public uint SizeX { get; private set; }
        public uint SizeY { get; private set; }


        public void AddBody(IBody body)
        {

        }
        public void RemoveBody(IBody body)
        {

        }

        public void Update()
        {

        }

        public void PrepareForNewGeneration()
        {

        }
    }
}
