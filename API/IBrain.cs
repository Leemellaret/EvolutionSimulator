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
        string Id { get;}
        /// <summary>
        /// Метод обработки входные данные мозгом. В результате выполнения массив Commands обновится.
        /// </summary>
        void Process(double[] data);

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
