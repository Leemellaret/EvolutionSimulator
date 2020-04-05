﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.WorldModels.Fields
{
    interface IField
    {
        /// <summary>
        /// Взаимодействие, которое данное поле содержит.
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Получить интенсивность взаимодействия с позиции существа, у которого есть сенсор этого поля.
        /// </summary>
        /// <param name="orientation">Позиция существа.</param>
        /// <param name="sensitivity">Чувствительность органа существа.</param>
        /// <returns>Интенсивность взаимодействия.</returns>
        double GetIntensityFromPosition(BodyOrientation orientation, uint sensitivity);

        /// <summary>
        /// Получить интенсивность взаимодействия в заданных точке и направлении.
        /// </summary>
        /// <param name="orientation">Точка и направление, в которой нужно получить интенсивность.</param>
        /// <returns>Интенсивность взаимодействия.</returns>
        double GetIntensityIn(BodyOrientation orientation);

        /// <summary>
        /// Создать источник взаимодействия в заданных точке и направлении.
        /// </summary>
        /// <param name="orientation">Точка и направление, в которой нужно создать источник взаимодействия.</param>
        /// <param name="value">Интенсивность источника.</param>
        void CreateIntensityIn(BodyOrientation orientation, double value);

        /// <summary>
        /// Удалить источник в заданных точке и направлении.
        /// </summary>
        /// <param name="orientation">Точка и направление, в которой нужно удалить источник взаимодействия.</param>
        void RemoveIntensityIn(BodyOrientation orientation);

        /// <summary>
        /// Изменить интенсивность в заданных точке и направлении на заданную величину.
        /// </summary>
        /// <param name="orientation">Точка и направление, в которых нужно измененить интенсивность.</param>
        /// <param name="dValue">Величина, на которую нужно изменить интенсивность.</param>
        void ChangeIntensityIn(BodyOrientation orientation, double dValue);

        /// <summary>
        /// Удалить все источники взаимодействия в этом поле.
        /// </summary>
        void ClearField();
    }
}
