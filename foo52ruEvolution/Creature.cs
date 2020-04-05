using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    class Creature : ICreature
    {
        private string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                body.Id = value;
                brain.Id = value;
            }
        }
        private Body body;
        private Brain brain;

        public IBody Body { get => body; }
        public IBrain Brain { get => brain; }

        public Creature(string id, Body body, Brain brain)
        {
            this.body = body;
            this.brain = brain;
            this.Id = id;
        }

        public IGenome GetGenome()
        {
            return null;
        }

        public void Interact()
        {
            while (true)
            {
                brain.Process(body.Data);
                body.InteractWithWorld(brain.Commands);
                if (body.Energy <= 0)
                {
                    body.Die();
                    break;
                }
                if (brain.GetCommand(0) < 24)
                    break;
            }
        }

        public override string ToString()
        {
            return $"ID={Id} E={body.Energy} (X,Y)=({body.Orientation.X},{body.Orientation.Y}) com={brain.GetCommand(0)} data={body.Data[0]}";
        }
    }
}
