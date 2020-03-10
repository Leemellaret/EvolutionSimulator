using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels
{
    interface IBrain
    {
        void Process(double[] input);
        int InputLength { get; }
        double[] Output { get; }
        int OutputLength { get; }
    }
}
