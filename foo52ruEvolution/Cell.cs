using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.foo52ruEvolution
{
    class Cell
    {
        public IEntity Entity { get; set; }

        public Cell(IEntity entity)
        {
            Entity = entity;
        }

        public override string ToString()
        {
            return Entity.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (!obj.GetType().Equals(this.GetType()))
                return false;

            return (obj as Cell).Entity.Equals(this.Entity);
        }

    }
}
