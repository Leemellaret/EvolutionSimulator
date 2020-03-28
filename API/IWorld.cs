﻿using System;
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
    }
}