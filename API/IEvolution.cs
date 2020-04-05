using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    public interface IEvolution
    {
        List<ICreature> MakeProgeny(List<ICreature> creatures);
        List<ICreature> MakeFirstPopulation(uint countOfCreatures);
    }
}
