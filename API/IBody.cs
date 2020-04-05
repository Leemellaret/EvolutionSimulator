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
        string Id { get; }
        /// <summary>
        /// Физический мир, где это тело находится.
        /// </summary>
        IWorld World { get; }

        /// <summary>
        /// Количество энергии у этого тела.
        /// </summary>
        double Energy { get; }

        /// <summary>
        /// Позиция этого тела в мире.
        /// </summary>
        BodyOrientation Orientation { get; }

        /// <summary>
        /// Информация, которую тело получило от взаимодействия с миром.
        /// </summary>
        double[] Data { get; }

        /// <summary>
        /// Сделать взаимодействие с миром.
        /// </summary>
        void InteractWithWorld(double[] commands);

        /// <summary>
        /// Совершает все необходимые изменения в программе, чтобы существо считалось мертвым
        /// </summary>
        void Die();
    }
}
