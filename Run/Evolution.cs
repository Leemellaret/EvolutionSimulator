using System;
using System.Collections.Generic;

namespace EvolutionSimulator.Run
{
    class Evolution
    {

        public double[] Alphas { get; }

        public Evolution() 
        {
            Alphas = new double[] { 2, 8 };
        }
        public List<Creature> MakeFirstPopulation()
        {
            Random r = new Random();
            var res = new List<Creature>(2);
            for (int i = 0; i < 2; i++)
            {
                double alpha = Alphas[i];// r.Next(MaxAlpha) + r.NextDouble();
                var brain = new Brain(alpha);
                res.Add(new Creature(i.ToString(), brain, alpha));
            }
            return res;
        }

        public List<Creature> MakeProgeny(List<Creature> creatures)
        {
            Random r = new Random();
            var creature = creatures[0];
            creature.Energy /= 2;
            creature.Health /= 2;
            double alpha = creature.Alpha;
            /*if (r.Next(100) < 20) 
            {
                alpha += r.NextDouble() / 2 - 0.5;
            }*/
            Brain brain = new Brain(alpha);
            return new List<Creature>() { new Creature($"{creature.Id}|{creature.CountOfReproductions}", brain, alpha, creature.Health, creature.Energy) };
        }
    }
}
