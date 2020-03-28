using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.CreatureModels;

namespace EvolutionSimulator.API
{
    public interface IBrain
    {
        /// <summary>
        /// Существо, у которого этот мозг.
        /// </summary>
        ICreature Creature { get; }

        /// <summary>
        /// Метод обработки входные данные мозгом. В результате выполнения массив Commands обновится.
        /// </summary>
        /// <param name="input">Входные данные, которые нужно обработать.</param>
        void Process(double[] input);

        /// <summary>
        /// Количество входов для входных данных этого мозга. 
        /// </summary>
        uint InputLength { get; }

        /// <summary>
        /// Команды, которые должны выполнить органы.
        /// </summary>
        double[] Commands { get; }

        /// <summary>
        /// Получить команду по индексу.
        /// </summary>
        /// <param name="index">Индекс нужной команды.</param>
        /// <returns>Команда, которую должен выполнить орган</returns>
        double GetCommand(uint index);

        /// <summary>
        /// Длина массива команд.
        /// </summary>
        uint CommandsLength { get; }
    }
}
