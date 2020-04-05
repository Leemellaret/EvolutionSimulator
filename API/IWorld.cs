using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels.BodyModels;

namespace EvolutionSimulator.API
{
    public interface IWorld
    {
        /// <summary>
        /// Тела, которые находятся и живут в этом мире.
        /// </summary>
        List<IBody> Bodies { get; }

        /// <summary>
        /// не включительно
        /// </summary>
        uint SizeX { get; }
        /// <summary>
        /// не включительно
        /// </summary>
        uint SizeY { get; }

        void AddBody(IBody body);
        void RemoveBody(IBody body);
        /// <summary>
        /// Например создать еду.
        /// </summary>
        void Update();
        void PrepareForNewGeneration();
    }
}
