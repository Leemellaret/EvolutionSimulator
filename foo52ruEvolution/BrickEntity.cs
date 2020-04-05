using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.foo52ruEvolution
{
    class BrickEntity : IEntity
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (obj.GetType().Equals(this.GetType()))
                return true;
            else
                return false;
        }
    }
}
