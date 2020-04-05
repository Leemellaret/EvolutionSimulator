using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    class Evolution : IEvolution
    {
        public World World { get; private set; }
        public double InitEnergy { get; private set; }

        private Random r;

        public Evolution(World world, double initEnergy)
        {
            World = world;
            InitEnergy = initEnergy;
            r = new Random();
        }
        public List<ICreature> MakeFirstPopulation(uint countOfCreatures)
        {
            var creatures = new List<ICreature>((int)countOfCreatures);
            int nx, ny;
            for (int i = 0; i < countOfCreatures; i++)
            {
                (nx, ny) = findRandomEmptyPlace();
                string id = i.ToString();
                Body body = new Body(id, World, InitEnergy, new BodyOrientation((uint)nx, (uint)ny, WorldDirection.all), r.Next(5) + 1);
                World.AddBody(body);

                int[] program = new int[Brain.lengthOfProgram];
                for (int j = 0; j < Brain.lengthOfProgram; j++)
                {
                    program[j] = r.Next(Brain.countOfCommands);
                }
                Brain brain = new Brain(id, program, r.Next(Brain.lengthOfProgram));
                creatures.Add(new Creature(id, body, brain));
            }
            return creatures;
        }
        public List<ICreature> MakeProgeny(List<ICreature> creatures)
        {
            creatures.ForEach(x => x.Body.Die());
            List<ICreature> nextGen = new List<ICreature>();
            foreach(var c in creatures)
            {
                nextGen.Add(MakeCopy(c as Creature, -1));
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    nextGen.Add(MakeCopy(creatures[i] as Creature, j));
                }
                nextGen.Add(MakeMutant(creatures[i] as Creature, 8));
            }
            return nextGen;
        }

        private Creature MakeCopy(Creature creature, int number)
        {
            int nx, ny;
            (nx, ny) = findRandomEmptyPlace();
            string id = creature.Id + (number >= 0 ? "|" + number.ToString() : "");
            Body newBody = new Body(
                id,
                World,
                InitEnergy,
                new BodyOrientation((uint)nx, (uint)ny, WorldDirection.all),
                (creature.Body as Body).InitData);
            World.AddBody(newBody);


            Brain newBrain = new Brain(
                id,
                (int[])(creature.Brain as Brain).Program.Clone(),
                (creature.Brain as Brain).InitCursor);
            return new Creature(id, newBody, newBrain);
        }
        private Creature MakeMutant(Creature creature, int number)
        {
            int nx, ny;
            (nx, ny) = findRandomEmptyPlace();
            string id = creature.Id + "|" + number.ToString();
            Body newBody = new Body(
                    id,
                    World,
                    InitEnergy,
                    new BodyOrientation((uint)nx, (uint)ny, WorldDirection.all),
                    (creature.Body as Body).InitData);
            World.AddBody(newBody);

            int[] newProgram = (int[])(creature.Brain as Brain).Program.Clone();
            newProgram[r.Next(Brain.lengthOfProgram)] = r.Next(Brain.countOfCommands);

            /*for (int j = 0; j < 8 && r.Next(2) == 1; j++)
            {
                newProgram[r.Next(Brain.lengthOfProgram)] = r.Next(Brain.countOfCommands);
            }*/
            Brain newBrain = new Brain(
                    id,
                    newProgram,
                    r.Next(3) == 2 ? r.Next(Brain.lengthOfProgram) : (creature.Brain as Brain).InitCursor);
            return new Creature(id, newBody, newBrain);
        }

        private (int,int) findRandomEmptyPlace()
        {
            int nx = r.Next((int)World.SizeX);
            int ny = r.Next((int)World.SizeY);
            while (!World.Cells[nx, ny].Entity.GetType().Equals(typeof(EmptyEntity)))
            {
                nx = r.Next((int)World.SizeX);
                ny = r.Next((int)World.SizeY);
            }
            return (nx, ny);
        }
    }
}
