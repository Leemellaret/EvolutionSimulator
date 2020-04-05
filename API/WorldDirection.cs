using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
    /// <summary>
    /// Направления в мире.
    /// </summary>
    public enum WorldDirection
    {
        /// <summary>
        /// Значит Y увеличивается.
        /// </summary>
        north,

        /// <summary>
        /// Значит X увеличивается.
        /// </summary>
        east,

        /// <summary>
        /// Значит Y уменьшается.
        /// </summary>
        south,

        /// <summary>
        /// Значит X уменьшается.
        /// </summary>
        west,

        /// <summary>
        /// Сразу все 4 направления. Не может быть использована для органов
        /// </summary>
        all
    }

    interface i1 { }
}
