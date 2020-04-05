using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.foo52ruEvolution
{
    class FoodEntity : IEntity
    {
        public uint Value { get; private set; }

        public FoodEntity(uint value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (!obj.GetType().Equals(this.GetType()))
                return false;

            return (obj as FoodEntity).Value == Value;
        }
    }
}
