
namespace EvolutionSimulator.Run
{
    class WorldData
    {
        public Cell[] Cells { get; set; }
        public WorldData(Cell[] cells)
        {
            Cells = cells;
        }
    }
}
