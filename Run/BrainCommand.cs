namespace EvolutionSimulator.Run
{
    public class BrainCommand
    {
        public CreatureAction Action { get; }
        public int Direction { get; }
        public BrainCommand(CreatureAction action, int direction)
        {
            Action = action;
            Direction = direction;
        }
    }

    public enum CreatureAction
    {
        Eat,
        Go,
        Hit,
        Reproduce
    }
}
