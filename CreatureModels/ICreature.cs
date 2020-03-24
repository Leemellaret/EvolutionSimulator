using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.BodyModels;
using EvolutionSimulator.BrainModels;
using EvolutionSimulator.EvolutionModels;

namespace EvolutionSimulator.CreatureModels
{
    public interface ICreature
    {
        IBody Body { get; }
        IBrain Brain { get; }
        IGenome GetGenome();

        void Interact();
    }
}
