using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.CreatureModels;
using EvolutionSimulator.BrainModels;
using EvolutionSimulator.BodyModels;
using EvolutionSimulator.BodyModels.BodyWithOrgansModel;

namespace EvolutionSimulator
{
    //
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class Brain : IBrain
    {
        public ICreature Creature { get; private set; }
        public void Process(double[] input)
        {
            Commands = input;
        }

        public Brain(uint inputLength)
        {
            InputLength = inputLength;
            CommandsLength = inputLength;

        }

        public uint InputLength { get; private set; }

        public double[] Commands { get; private set; }

        public double GetCommand(uint index)
        {
            return Commands[index];
        }

        public uint CommandsLength { get; private set; }
    }
}
