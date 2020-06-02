namespace EvolutionSimulator.Run
{
    class Interaction
    {
        public CreatureAction Action { get; }
        public int Direction { get; }
        public double Value { get; }

        public Interaction(CreatureAction action, int direction, double value)
        {
            Action = action;
            Direction = direction;
            Value = value;
        }
    }
}
