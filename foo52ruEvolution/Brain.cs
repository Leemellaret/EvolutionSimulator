using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    class Brain : IBrain
    {
        public string Id { get; set; }
        public uint InputLength { get; private set; }
        public uint CommandsLength { get; private set; }
        private double command;
        public int[] Program { get; private set; }
        public int CursorInProgram { get; private set; }
        public const int countOfCommands = 64;
        public const int lengthOfProgram = 64;
        public int InitCursor { get; private set; }

        public Brain(string id, int[] program, int cursor)
        {
            InputLength = 1;
            CommandsLength = 1;
            command = 0;
            Program = program;
            CursorInProgram = cursor;
            InitCursor = cursor;
        }

        public double[] Commands { get=>new double[] { command }; }
        public double GetCommand(uint i)
        {
            if (i == 0)
                return command;
            else throw new ArgumentOutOfRangeException("i", "i != 0");
        }
        public void Process(double[] data)
        {
            int d = (int)(data[0]);
            CursorInProgram = (CursorInProgram + d) % lengthOfProgram;
            if (d != 1 && d != 2 && d != 3 && d != 4 && d != 5)
            {
                int ksafj = 893749;
            }
            int countOfJumpes = 0;
            while (Program[CursorInProgram] >= 24)
            {
                d = Program[CursorInProgram];
                CursorInProgram = (CursorInProgram + d) % lengthOfProgram;

                countOfJumpes++;
                if (countOfJumpes == 10) 
                {
                    command = 8;
                    return;
                }
            }

            command = Program[CursorInProgram];
        }
    }
}
