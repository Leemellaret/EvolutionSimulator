using System.Collections.Generic;
using System.Linq;

namespace EvolutionSimulator.Run
{
    class Brain
    {
        public double Alpha { get; }
        private double e1 { get; }
        private double e2 { get; }
        private double h1 { get; }
        private double absorbAble { get; }

        public BrainState State { get; private set; }

        public Brain(double alpha)
        {
            this.Alpha = alpha;
            h1 = CreatureParameters.h1(alpha);
            e1 = CreatureParameters.e1(alpha);
            e2 = CreatureParameters.e2(alpha);
            absorbAble = CreatureParameters.AbsorbAble(alpha);
            State = BrainState.Normal;

        }

        private BrainCommand CommandInNormalState(BrainInput data)
        {
            if (data.Health < h1)
            {
                State = BrainState.LowHealth;
                return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
            }
            else if (data.Energy < e1)
            {
                State = BrainState.LowEnergy;
                return new BrainCommand(CreatureAction.Eat, 0);
            }
            else
            {
                if (ContainsMorePowerfulCreature(data.Cells[0].StandingCreatures))
                //на клетке жука стоит жук сильнее
                {
                    return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
                }
                else if (data.Cells[0].StandingCreatures.Count > 1 && data.Cells[0].StandingCreatures.Count(x => x.Alpha != Alpha) > 0)
                {
                    return new BrainCommand(CreatureAction.Hit, 0);
                }
                else if (data.Energy > e2)
                {
                    int emptyCell = FindSafeCellForChild(data.Cells);
                    if (emptyCell >= 0)
                    {
                        return new BrainCommand(CreatureAction.Reproduce, emptyCell);
                    }
                    else
                    {
                        State = BrainState.Reproduction;
                        return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
                    }
                }
                else if (data.Cells[0].FoodCount < CreatureParameters.FoodAbsorbCostPerUnit * absorbAble)
                //нет еды на своей клетке
                {
                    return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
                }
                else
                {
                    return new BrainCommand(CreatureAction.Eat, 0);
                }

            }
        }
        private BrainCommand CommandInReproductionState(BrainInput data)
        {
            int emptyCellDirection;
            if (data.Health < h1)
            {
                State = BrainState.LowHealth;
                return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
            }
            else if (data.Energy <= e2)
            {
                State = BrainState.Normal;
                return CommandInNormalState(data);
            }
            else if((emptyCellDirection = FindSafeCellForChild(data.Cells)) >= 0)
            {
                State = BrainState.Normal;
                return new BrainCommand(CreatureAction.Reproduce, emptyCellDirection);
            }
            else if(data.Cells[0].StandingCreatures.Count == 1)
            {
                return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
            }
            else
            {
                return new BrainCommand(CreatureAction.Hit, 0);
            }
        }
        private BrainCommand CommandInLowEnergyState(BrainInput data)
        {
            if (data.Health < h1)
            {
                State = BrainState.LowHealth;
                return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
            }
            else if (data.Energy >= e1)
            {
                State = BrainState.Normal;
                return CommandInNormalState(data);
            }
            else
            {
                if (data.Cells[0].FoodCount > 0.3 * absorbAble)
                {
                    return new BrainCommand(CreatureAction.Eat, 0);
                }
                else
                {
                    return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
                }
            }
        }
        private BrainCommand CommandInLowHealthState(BrainInput data)
        {
            if (data.Health >= h1)
            {
                if (data.Energy > e2)
                {
                    State = BrainState.Reproduction;
                    return CommandInReproductionState(data);
                }
                else if (data.Energy < e1)
                {
                    State = BrainState.LowEnergy;
                    return CommandInLowEnergyState(data);
                }
                else
                {
                    State = BrainState.Normal;
                    return CommandInNormalState(data);
                }
            }
            else
                return new BrainCommand(CreatureAction.Go, FindMostSafeCell(data.Cells));
        }
        public BrainCommand Process(BrainInput data)
        {

            if(State == BrainState.Normal)
            {
                return CommandInNormalState(data);
            }
            else if(State == BrainState.Reproduction)
            {
                return CommandInReproductionState(data);
            }
            else if(State == BrainState.LowEnergy)
            {
                return CommandInLowEnergyState(data);
            }
            else
            {
                return CommandInLowHealthState(data);
            }
        }

        private int FindMostSafeCell(Cell[] cells)
        {
            var c = new List<int>(5);
            int res;
            if (cells[0].StandingCreatures.Count == 1)
                c.Add(0);

            
            for (int i = 1; i < cells.Length; i++)
            {
                if (cells[i] != null && cells[i].StandingCreatures.Count == 0)
                    c.Add(i);
            }
            res = FindMaxFoodCell(cells, c);
            if (res >= 0)
                return res;


            c.Clear();
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != null && !ContainsMorePowerfulCreature(cells[i].StandingCreatures))
                    c.Add(i);
            }
            res = FindMaxFoodCell(cells, c);
            if (res >= 0)
                return res;

            c.Clear();
            int minCountOfCreatures = findMinCountOfCreatures(cells);
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != null && cells[i].StandingCreatures.Count == minCountOfCreatures)
                    c.Add(i);
            }
            return FindMaxFoodCell(cells, c);
        }
        private int findMinCountOfCreatures(Cell[] cells)
        {
            int minCount = cells[0].StandingCreatures.Count;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != null && minCount > cells[i].StandingCreatures.Count)
                    minCount = cells[i].StandingCreatures.Count;
            }
            return minCount;
        }
        private int FindMaxFoodCell(Cell[] cells, List<int> indexes)
        {
            int index = -1;
            double maxFood = -1;
            for (int i = 0; i < indexes.Count; i++)
            {
                if (cells[i] != null && maxFood < cells[indexes[i]].FoodCount)
                {
                    index = indexes[i];
                    maxFood = cells[indexes[i]].FoodCount;
                }
            }
            return index;
        }
        private bool ContainsMorePowerfulCreature(List<Creature> creatures)
        {
            foreach(Creature c in creatures)
            {
                if (c.Alpha / Alpha >= CreatureParameters.size1)
                    return true;
            }
            return false;
        }
        private int FindSafeCellForChild(Cell[] cells)
        {
            for (int i = 1; i < cells.Length; i++)
            {
                if (cells[i] != null && cells[i].StandingCreatures.Count(x=>x.Alpha != Alpha) == 0)
                    return i;
            }
            return -1;
        }

        public override string ToString()
        {
            return $"alpha={Alpha} CurrentState={State}";
        }
    }

    public enum BrainState
    {
        Normal,
        LowHealth,
        LowEnergy,
        Reproduction
    }
}
