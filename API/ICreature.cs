using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    public interface ICreature
    {
        string Id { get; }

        IBrain Brain { get; }

        /// <summary>
        /// Сделать взаимодействие с миром.
        /// </summary>
        IInteraction Interact(IWorldData world);
    }
}
