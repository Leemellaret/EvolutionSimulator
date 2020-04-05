using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.WorldModels;
using EvolutionSimulator.CreatureModels;
using EvolutionSimulator.API;

namespace EvolutionSimulator.API
{
    public class RealtimeSimulator
    {
        public IWorld World { get; private set; }
        public IEvolution Evolution { get; private set; }
        private List<ICreature> creatures;

        public RealtimeSimulator(IWorld world, IEvolution evolution, List<ICreature> creatures)
        {
            if (creatures.All(cr => cr.Body.World.Equals(world)))
                throw new ArgumentException("Не все существа находятся в поданном мире world", "creatures");

            World = world;
            Evolution = evolution;
            this.creatures = new List<ICreature>(creatures);
        }

        public void MakeOneTick()
        {
            foreach(var creature in creatures)
            {
                creature.Interact();
                if (!World.Bodies.Contains(creature.Body))//Если мир не содержит тело этого существа, значит оно умерло
                {
                    Creatures.Remove(creature);
                }
            }
        }

        public List<ICreature> Creatures { get => creatures; }
    }
}
