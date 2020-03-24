using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BrainModels;
using EvolutionSimulator.BodyModels;
using EvolutionSimulator.EvolutionModels;


namespace EvolutionSimulator.CreatureModels
{
    public class Creature : ICreature
    {
        public IBrain Brain { get; private set; }
        public IBody Body { get; private set; }

        public Creature(IBody body, IBrain brain)
        {
            Brain = brain;
            Body = body;
        }

        public IGenome GetGenome()
        {
            return null;
        }

        public void Interact()
        {

        }
    }
}
