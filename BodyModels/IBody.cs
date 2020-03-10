using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels
{
    interface IBody
    {
        double Energy { get; }
        double Health { get; }
        Orientation Orientation { get; }
    }
}
