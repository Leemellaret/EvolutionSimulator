using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels.Fields
{
    public class FoodField : IField
    {
        /// <summary>
        /// Массив источников взаимодействия. 0dim=x 1dim=y 2dim=orientation
        /// </summary>
        double[,,] intensitySources;
        uint worldSizeX;
        uint worldSizeY;
        public string FieldName { get; private set; }

        public FoodField(uint worldSizeX, uint worldSizeY)
        {
            intensitySources = new double[worldSizeX, worldSizeY, 5];
            this.worldSizeX = worldSizeX;
            this.worldSizeY = worldSizeY;
            FieldName = "food";
        }
        public double GetIntensityFromPosition(Orientation orientation, uint sensitivity)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (orientation.Direction == WorldDirection.all)
                throw new ArgumentException("Невозможно получить интенсивность взаимодействия с WorldDirection.all.", "orientation");

            double intensitySum = 0;
            uint x = orientation.X;
            uint y = orientation.Y;

            if (orientation.Direction == WorldDirection.north)
            {
                for (uint iy = orientation.Y; iy - y <= sensitivity && iy < worldSizeY; iy++)
                {
                    intensitySum += intensitySources[x, iy, (int)WorldDirection.south] + intensitySources[x, iy, (int)WorldDirection.all];
                }
            }
            else if (orientation.Direction == WorldDirection.east)
            {
                for (uint ix = orientation.X; ix - x <= sensitivity && ix < worldSizeX; ix++)
                {
                    intensitySum += intensitySources[ix, y, (int)WorldDirection.west] + intensitySources[ix, y, (int)WorldDirection.all];
                }
            }
            else if (orientation.Direction == WorldDirection.south)
            {
                for (uint iy = orientation.Y; y - iy <= sensitivity && iy >= 0; iy--)
                {
                    intensitySum += intensitySources[x, iy, (int)WorldDirection.north] + intensitySources[x, iy, (int)WorldDirection.all];
                }
            }
            else if (orientation.Direction == WorldDirection.west)
            {
                for (uint ix = orientation.X; x - ix <= sensitivity && ix >= 0; ix--)
                {
                    intensitySum += intensitySources[ix, y, (int)WorldDirection.south] + intensitySources[ix, y, (int)WorldDirection.all];
                }
            }


            return intensitySum;
        }

        public double GetIntensityIn(Orientation orientation)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (orientation.X >= worldSizeX || orientation.Y >= worldSizeY)
                throw new ArgumentException("Координаты позиции выходят за пределы мира.", "orientation");

            return intensitySources[orientation.X, orientation.Y, (int)orientation.Direction];
        }
        public void CreateIntensityIn(Orientation orientation, double value)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (orientation.X >= worldSizeX || orientation.Y >= worldSizeY)
                throw new ArgumentException("Координаты позиции выходят за пределы мира.", "orientation");

            if (intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] > 0)
                throw new InvalidOperationException("В этой точке с таким направлением уже есть источник взаимодействия.");

            intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] = value;
        }
        public void RemoveIntensityIn(Orientation orientation)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (orientation.X >= worldSizeX || orientation.Y >= worldSizeY)
                throw new ArgumentException("Координаты позиции выходят за пределы мира.", "orientation");

            intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] = 0;
        }
        public void ChangeIntensityIn(Orientation orientation, double dValue)
        {
            if (orientation == null)
                throw new ArgumentNullException("orientation");
            if (orientation.X >= worldSizeX || orientation.Y >= worldSizeY)
                throw new ArgumentException("Координаты позиции выходят за пределы мира.", "orientation");

            if (intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] == 0)
                throw new InvalidOperationException("В этой точке с таким направлением нет источника взаимодействия.");

            if (intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] + dValue < 0)
                throw new InvalidOperationException("Интенсивность источника меньше чем значение, на которое его нужно уменьшить.");

            intensitySources[orientation.X, orientation.Y, (int)orientation.Direction] += dValue;

        }
        public void ClearField()
        {
            intensitySources = new double[worldSizeX, worldSizeY, 5];
        }
    }
}
