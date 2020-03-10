using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.Automaton
{
    interface IAutomaton
    {
        AutomatonIO Process(AutomatonIO input);
        SymbolType[] InputSymbolTypes { get; }
        int InputLength { get; }
        SymbolType[] OutputSymbolTypes { get; }
        int OutputLength { get; }
        AutomatonTableKV DefaultOutput { get; }
        SymbolType States { get; }
        int CurrentState { get; }
    }
}
