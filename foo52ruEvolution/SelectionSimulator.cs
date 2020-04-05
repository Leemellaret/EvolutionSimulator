using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    public class SelectionSimulator : ISimulator
    {
        public IWorld World { get; private set; }
        public IEvolution Evolution { get; private set; }
        public bool IsSelectionTime { get; private set; }
        public List<ICreature> Creatures { get; private set; }

        public SelectionSimulator(IWorld world, IEvolution evolution, List<ICreature> creatures)
        {
            if (!creatures.All(cr => cr.Body.World.Equals(world)))
                throw new ArgumentException("Не все существа находятся в поданом мире world", "creatures");

            World = world;
            Evolution = evolution;
            Creatures = new List<ICreature>(creatures);
            IsSelectionTime = false;
        }

        public void MakeOneTick()
        {
            int i = 0;
            while (i < Creatures.Count)
            {
                var creature = Creatures[i];
                i++;
                //Console.WriteLine($"{creature.ToString()}");
                creature.Interact();

                if (!World.Bodies.Contains(creature.Body))//Если мир не содержит тело этого существа, значит оно умерло
                {
                    Creatures.Remove(creature);
                    i--;
                }

                if (Creatures.Count == 8)
                {
                    IsSelectionTime = true;
                    break;
                }
            }
            World.Update();
        }

        public void MakeSelection()
        {
            World.PrepareForNewGeneration();
            foreach (var c in Creatures)
                c.Id += "+";
            Creatures = Evolution.MakeProgeny(Creatures);
            World.Update();
            IsSelectionTime = false;
        }
    }
}
