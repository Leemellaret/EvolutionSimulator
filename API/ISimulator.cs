using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    public interface ISimulator
    {
        List<ICreature> Creatures { get; }
        IWorld World { get; }
        bool IsSelectionTime { get; }
        IEvolution Evolution { get; }
        void MakeOneTick();
        void MakeSelection();
    }
}
