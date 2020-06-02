using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionSimulator.Run
{
    class World
    {
        public Evolution Evolution { get; }
        public Cell[,] Cells { get; }
        private List<(Creature, Orientation)> creatures;

        public World(Evolution evolution, Cell[,] cells)
        {
            Evolution = evolution;
            Cells = cells;
            creatures = new List<(Creature, Orientation)>();
            SizeX = cells.GetLength(0);
            SizeY = cells.GetLength(1);

            CountOfActions = new uint[4];
        }

        public void AddCreatures(params (Creature, Orientation)[] creatures)
        {
            foreach (var creature in creatures)
                AddCreature(creature.Item1, creature.Item2);
        }

        public void AddCreature(Creature creature, Orientation orientation)
        {
            if (creatures.FindIndex(x => x.Item1.Id == creature.Id) >= 0)
                throw new ArgumentException($"Существо с таким Id={creature.Id} уже существует в этом мире", "creature");


            Cells[orientation.X, orientation.Y].StandingCreatures.Add(creature as Creature);
            creatures.Add((creature as Creature, orientation));
        }

        public void RemoveCreature(int creatureIndex)
        {
            var c = creatures[creatureIndex].Item1;
            var o = creatures[creatureIndex].Item2;
            Cells[o.X, o.Y].StandingCreatures.Remove(c);
            creatures.RemoveAt(creatureIndex);
        }

        private static (int, int)[] dc = new (int, int)[] {
            (0, 0),
            (-1, 0),
            (0, 1),
            (1, 0),
            (0, -1)
        };
        private bool IsValidCoordinates(int x, int y)
        {
            return 0 <= x && x < SizeX && 0 <= y && y < SizeY;
        }
        public bool CanMoveTo(Orientation o, int dir)
        {
            if (dir == 0)
                return true;

            int cx = o.X + dc[dir].Item1;
            int cy = o.Y + dc[dir].Item2;
            return IsValidCoordinates(cx, cy);
        }
        public void Move(int creatureIndex, int dir)
        {
            var c = creatures[creatureIndex].Item1;
            var o = creatures[creatureIndex].Item2;
            if (CanMoveTo(o, dir))
            {
                Cells[o.X, o.Y].StandingCreatures.Remove(c);
                o = o.MovePoint(dc[dir].Item1, dc[dir].Item2);
                Cells[o.X, o.Y].StandingCreatures.Add(c);
                creatures[creatureIndex] = (c, o);
            }
        }
        public double AbsorbFood(Orientation o, double count)
        {
            double absorbed = Cells[o.X, o.Y].GiveFood(count);
            TotalFoodAbsorbed += absorbed;
            return absorbed;
        }
        /*public void DoHit(Orientation2DCell orientationOfHitter, double force)
        {
            var c = Cells[orientationOfHitter.X, orientationOfHitter.Y].StandingCreature;
            if (c != null)
                c.Shield -= force;
        }*/
        private Cell[] getCellsAround(Orientation o)
        {
            var cells = new Cell[dc.Length];
            for (int i = 0; i < dc.Length; i++)
            {
                int x = o.X + dc[i].Item1;
                int y = o.Y + dc[i].Item2;
                if (IsValidCoordinates(x, y))
                {
                    cells[i] = Cells[x, y];
                }
                else
                {
                    //cells[i] = new Cell(-1, -1, true);
                    cells[i] = null;
                }
            }
            return cells;
        }

        public uint[] CountOfActions { get; private set; }
        public static uint[] Deaths { get; } = new uint[2];
        public double TotalFood
        {
            get
            {
                double res = 0;
                foreach (var cell in Cells)
                {
                    res += cell.FoodCount;
                }
                return res;
            }
        }
        public double TotalFoodAbsorbed
        {
            get;
            private set;
        }
        public double UniformityOfDistribution()
        {
            if (CountOfCreatures == 0)
                return 0;

            double res = 0;
            int countLiveCells = 0;
            double littleAlpha = Evolution.Alphas[0];
            foreach (Cell c in Cells)
            {
                int countOfAll = c.StandingCreatures.Count;
                int countOfLittle = c.StandingCreatures.Count(x => x.Alpha == littleAlpha);
                int countOfBig = countOfAll - countOfLittle;

                if (countOfAll > 0)
                {
                    res += Math.Abs(countOfLittle - countOfBig) / countOfAll;
                    countLiveCells++;
                }
            }
            return res / countLiveCells;
        }

        private void update()
        {
            for(int i = 0; i < SizeX; i++)
            {
                for(int j = 0; j < SizeY; j++)
                {
                    Cells[i, j].Update();
                }
            }
        }
        public void MakeInteractions()
        {
            List<CreatureChange> changes = new List<CreatureChange>(creatures.Count);
            for (int i = 0; i < creatures.Count; i++)
            {
                changes.Add(new CreatureChange(this, i, -1, -1, 0, 0));
            }

            TotalFoodAbsorbed = 0;

            for (int i = 0; i < creatures.Count; i++)
            {
                var c = creatures[i].Item1;
                var o = creatures[i].Item2;
                WorldData data = new WorldData(getCellsAround(o));

                var interaction = c.Interact(data);
                if (interaction != null)
                {
                    CountOfActions[(int)interaction.Action]++;
                    //Logger.Log($"world> creature id={c.Id} do {act.Action} value={act.Value} dir={act.Direction}");
                    if (interaction.Action == CreatureAction.Go)
                    {
                        changes[i].MoveDirection = interaction.Direction;
                    }
                    else if (interaction.Action == CreatureAction.Eat)
                    {
                        changes[i].EnergyAddition = AbsorbFood(creatures[i].Item2, interaction.Value);
                    }
                    else if (interaction.Action == CreatureAction.Hit)
                    {
                        var creaturesInCell = Cells[o.X, o.Y].StandingCreatures;
                        for (int j = 0; j < creaturesInCell.Count; j++)
                        {
                            var ind = creatures.FindIndex(x => x.Item1.Equals(creaturesInCell[j]));
                            if (ind != i && creatures[i].Item1.Alpha != creatures[ind].Item1.Alpha)
                                changes[ind].HealthLost += interaction.Value;
                        }
                    }
                    else //if (act.Action == CreatureAction.Reproduce)
                    {
                        changes[i].ReproduceDirection = interaction.Direction;
                    }
                }
                /*else
                    Logger.Log($"world> creature id={c.Id} died and do nothing");*/
            }

            for (int i = 0; i < changes.Count; i++)
            {
                changes[i].Apply();
            }

            for (int i = 0; i < creatures.Count; i++)
            {
                if (!creatures[i].Item1.IsAlive)
                    RemoveCreature(i);
            }


            //Logger.Log($"\nCreatures States after interactions:\n{StatesOfCreatures()}");
            update();
        }
        public string StatesOfCreatures()
        {
            string res = "";

            for (int i = 0; i < creatures.Count; i++)
            {
                var c = creatures[i];
                res += $"{c.Item1} {c.Item2}\n";
            }

            return res;
        }

        public int CountOfCreatures { get => creatures.Count; }
        public int CountOfCreaturesWithAlpha(double alpha)
        {
            return creatures.Count(x => x.Item1.Alpha == alpha);
        }

        public List<Creature> Creatures { get => creatures.Select(x => x.Item1).ToList(); }
        public int SizeX { get; }
        public int SizeY { get; }

        private class CreatureChange
        {
            public World World;
            public int CreatureIndex;
            public int MoveDirection;
            public int ReproduceDirection;
            public double EnergyAddition;
            public double HealthLost;

            public CreatureChange(World world, int creatureIndex, int moveDir, int reproduceDirection, double energyAddition, double healthLost)
            {
                World = world;
                CreatureIndex = creatureIndex;
                MoveDirection = moveDir;
                ReproduceDirection = reproduceDirection;
                EnergyAddition = energyAddition;
                HealthLost = healthLost;
            }

            public void Apply()
            {
                var c = World.creatures[CreatureIndex];
                
                c.Item1.Energy += EnergyAddition;
                c.Item1.Health -= HealthLost;
                if (MoveDirection != -1)
                    World.Move(CreatureIndex, MoveDirection);
                if (ReproduceDirection != -1)
                {
                    var parent = World.creatures[CreatureIndex];
                    var child = World.Evolution.MakeProgeny(new List<Creature>() { parent.Item1 })[0];
                    var childOrientation = parent.Item2.MovePoint(dc[ReproduceDirection].Item1, dc[ReproduceDirection].Item2);
                    World.AddCreature(child, childOrientation);
                }
            }
        }
    }
}
