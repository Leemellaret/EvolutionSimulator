using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;
using EvolutionSimulator.BrainModels;
using EvolutionSimulator.API;

namespace EvolutionSimulator.API
{
    public interface ICreature
    {
        string Id { get; set; }
        IBody Body { get; }
        IBrain Brain { get; }
        IGenome GetGenome();

        void Interact();
    }
}
