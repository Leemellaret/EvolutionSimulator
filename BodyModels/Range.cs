using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels
{
    public class Range
    {
        public uint Begin { get; private set; }
        public uint End { get; private set; }

        public Range(uint begin, uint end)
        {
            if (begin > end)
                throw new ArgumentOutOfRangeException("end", "begin > end");

            Begin = begin;
            End = end;
        }
    }
}
