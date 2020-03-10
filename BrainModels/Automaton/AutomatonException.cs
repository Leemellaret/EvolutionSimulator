using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels.Automaton
{
    [Serializable]
    public class AutomatonException : Exception
    {
        public AutomatonException() { }
        public AutomatonException(string message) : base(message) { }
        public AutomatonException(string message, Exception inner) : base(message, inner) { }
        protected AutomatonException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "Automaton Exception: " + Message;
        }
    }
}
