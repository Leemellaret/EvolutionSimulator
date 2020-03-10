using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BrainModels
{

    [Serializable]
    public class BrainException : Exception
    {
        public BrainException() { }
        public BrainException(string message) : base(message) { }
        public BrainException(string message, Exception inner) : base(message, inner) { }
        protected BrainException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return "Brain Exception: " + Message;
        }
    }
}
