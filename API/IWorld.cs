using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    public interface IWorld
    {
        IEvolution Evolution { get; }
        /// <summary>
        /// Существа, которые находятся и живут в этом мире.
        /// </summary>
        List<ICreature> Creatures { get; }

        /// <summary>
        /// Добавить существо в мир. Его Id должен быть уникальным.
        /// </summary>
        /// <param name="creature">Существо, которое нужно добавить.</param>
        /// <param name="orientation">Позиуия существа в мире.</param>
        void AddCreature(ICreature creature, IOrientation orientation);
        
        /// <summary>
        /// Удалить существо из мира.
        /// </summary>
        /// <param name="creature">Существо, которое нужно удалить.</param>
        void RemoveCreature(int creatureIndex);

        void MakeInteractions();
    }
}
