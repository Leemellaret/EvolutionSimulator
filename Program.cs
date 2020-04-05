using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.foo52ruEvolution;
using EvolutionSimulator.API;
using System.Windows.Forms;
using System.IO;

namespace EvolutionSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            var s = Input();
            Random r = new Random();
            uint countOfCreatures = (uint)s[0];
            uint wSizeX = (uint)s[1], wSizeY = (uint)s[2];

            IWorld world = new World(wSizeX, wSizeY);
            var evolution = new Evolution(world as World, s[3]);
            var creatures = evolution.MakeFirstPopulation(countOfCreatures);
            world.Update();
            SelectionSimulator ss = new SelectionSimulator(world, evolution, creatures.Select(x=>x as ICreature).ToList());

            ulong genNum = 0;
            ulong maxCountOfSteps = 0;
            ulong t = 1000;
            while (true)
            {
                ulong countOfSteps = 0;
                genNum++;
                while (!ss.IsSelectionTime)
                {
                    ss.MakeOneTick();
                    countOfSteps++;
                }
                ss.MakeSelection();
                if (countOfSteps > maxCountOfSteps)
                    maxCountOfSteps = countOfSteps;
                Console.WriteLine($"gennum={genNum:000000} steps={countOfSteps:0000000} maxSteps={maxCountOfSteps:0000000}");
            }

            //Visualization v = new Visualization(ss, s[4], s[4] * (int)wSizeX + s[5], s[4] * (int)wSizeY + s[6], (ulong)s[7]);
            //v.StartPosition = FormStartPosition.Manual;
            //Application.Run(v);
        }

        static void printListOfCreatures(List<ICreature> creatures, uint numberOfGeneration)
        {
            Console.WriteLine($"gen no = {numberOfGeneration}");
            foreach (var c in creatures)
                Console.WriteLine((c.Body as Body).Id);
            Console.WriteLine("------------------------------");
        }

        static int[] Input()
        {
            return File.ReadAllLines(@"d:\1\ess.txt").Select(x => int.Parse(x.Substring(4))).ToArray();

        }
    }
}
