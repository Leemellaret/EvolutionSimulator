using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    public interface ISimulator
    {
        IWorld World { get; }
        IEvolution Evolution { get; }
        bool IsSelectionTime { get; }
        void MakeOneTick();
        void MakeSelection();
    }
}
