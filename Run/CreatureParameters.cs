namespace EvolutionSimulator.Run
{
    static class CreatureParameters
    {
        public const double size1 = 1.1;

        public const double OneHitForceCost = 0.01;
        public const double OneAbsorbAbleCost = 0.01;
        public const double OneMaxShieldCost = 0.01;
        public const double OneRegenerationValueCost = 0.01;

        public const double RegenerationCostPerUnit = 2;
        public const double MoveCostPerStep = 2;
        public const double FoodAbsorbCostPerUnit = 0.3;
        public const double HitCostPerUnit = 3;

        public static double MaxHealth(double size)
        {
            return 100 * size;
        }
        public static double h1(double size)
        {
            return 0.3 * MaxHealth(size);
        }
        public static double RegenerationValue(double size)
        {
            return 0.01 * MaxHealth(size);
        }

        public static double InitEnergy(double size)
        {
            return 100 * size;
        }
        public static double e1(double size)
        {
            return 0.3 * InitEnergy(size);
        }
        public static double e2(double size)
        {
            return 2 * InitEnergy(size);
        }
        public static double AbsorbAble(double size)
        {
            return 0.1 * InitEnergy(size);
        }

        public static double HitForce(double size)
        {
            return 20 * size;
        }
    }
}
