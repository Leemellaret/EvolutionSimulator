using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels.BodyModels.BodyWithOrgansModel.Organs;
using EvolutionSimulator.CreatureModels;
using EvolutionSimulator.API;

namespace EvolutionSimulator.API
{
    public interface IBody
    {
        /// <summary>
        /// Физический мир, где это тело находится.
        /// </summary>
        IWorld World { get; }

        /// <summary>
        /// Существо, у которого это тело.
        /// </summary>
        ICreature Creature { get; }

        /// <summary>
        /// Количество энергии у этого тела.
        /// </summary>
        double Energy { get; }

        /// <summary>
        /// Позиция этого тела в мире.
        /// </summary>
        Orientation Orientation { get; }

        /// <summary>
        /// Информация, которую тело получило от взаимодействия с миром.
        /// </summary>
        double[] Data { get; }

        /// <summary>
        /// Сделать взаимодействие с миром.
        /// </summary>
        void InteractWithWorld();
    }
}
