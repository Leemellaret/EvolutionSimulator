using System.Collections.Generic;

namespace EvolutionSimulator.Run
{
    class Cell
    {
        private double foodCount;
        private double foodAbsorbed;
        public double FoodCount 
        { 
            get=>foodCount;
            private set
            {
                if (0 <= value && (value <= MaxFoodCount || MaxFoodCount<0))
                {
                    foodCount = value;
                }
                else if (value < 0)
                {
                    foodCount = 0;
                }
                else //if (value > MaxFoodCount)
                {
                    foodCount = MaxFoodCount;
                }
            }
        }
        public double MaxFoodCount { get; }
        public double FoodIncrease { get; }
        public List<Creature> StandingCreatures { get; internal set; }

        public Cell(double foodCount, double foodIncrease, double maxFoodCount=-1)
        {
            MaxFoodCount = maxFoodCount;
            FoodCount = foodCount >= 0 ? foodCount : 0;
            FoodIncrease = foodIncrease >= 0 ? foodIncrease : 0;
            StandingCreatures = new List<Creature>();

            foodAbsorbed = 0;
        }

        private static double availableAbsorb(double foodCount, double requiredFood)
        {
            if (requiredFood > foodCount)
            {
                return foodCount;
            }
            else
            {
                return requiredFood;
            }
        }

        internal double GiveFood(double requiredCount)
        {
            double res = availableAbsorb(FoodCount / StandingCreatures.Count, requiredCount);
            foodAbsorbed += res;
            return res;
        }

        internal void AddExtraFood(double value)
        {
            if (value >= 0)
                FoodCount += value;
        }

        internal void Update()
        {
            FoodCount += FoodIncrease - foodAbsorbed;
            foodAbsorbed = 0;
        }

        public override string ToString()
        {
            return $"FoodCount={FoodCount} FoodIncrease={FoodIncrease} Creatures={StandingCreatures.Count}";
        }
    }
}
