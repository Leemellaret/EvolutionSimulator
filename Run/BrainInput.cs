namespace EvolutionSimulator.Run
{
    class BrainInput
    {
        public double Health { get; }
        public double Energy { get; }
        public Cell[] Cells { get; }
        public BrainInput(double shield, double energy, Cell[] cells)
        {
            Health = shield;
            Energy = energy;
            Cells = cells;
        }
    }
}
