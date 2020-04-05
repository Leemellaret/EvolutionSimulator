using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolutionSimulator.foo52ruEvolution
{
    public partial class Visualization : Form
    {
        public SelectionSimulator ss { get; private set; }
        public int WSizeX { get; private set; }
        public int WSizeY { get; private set; }
        public int CellSide { get; private set; }
        public ulong StartDrawing { get; private set; }

        int CellBorderWidth { get => 1/*CellSide / 10*/; }
        public Visualization()
        {
            InitializeComponent();
        }
        public Visualization(SelectionSimulator simulator, int cellSide, int sizex, int sizey, ulong startDrawing)
        {
            InitializeComponent();
            ss = simulator;
            WSizeX = (int)simulator.World.SizeX;
            WSizeY = (int)simulator.World.SizeY;
            CellSide = cellSide;
            Size = new Size(sizex, sizey);
            StartDrawing = startDrawing;
            /*float c = 1.07f;
            Size = new Size((int)(c * CellSide * WSizeX), (int)(c * CellSide * WSizeY));*/
        }

        private void Visualization_Shown(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            ulong genNum = 0;
            ulong maxCountOfSteps = 0;
            ulong t = 1000;
            while (true)
            {
                ulong countOfSteps = 0;
                genNum++;
                if(genNum>= StartDrawing/* || genNum % t == 0*/) 
                    drawClearWorld(g);
                while (!ss.IsSelectionTime)
                {
                    if (genNum >= StartDrawing/* || genNum % t == 0*/)
                        drawEntities(g);
                    //System.Threading.Thread.Sleep(100);
                    ss.MakeOneTick();
                    countOfSteps++;
                    //Console.ReadKey();
                }
                /*Console.WriteLine("Selection Time");
                foreach (var c in ss.Creatures)
                    Console.WriteLine(c.Id);
                Console.WriteLine("--------------------------------------");*/
                ss.MakeSelection();
                if (countOfSteps > maxCountOfSteps)
                    maxCountOfSteps = countOfSteps;
                Console.WriteLine($"gennum={genNum} steps={countOfSteps} maxSteps={maxCountOfSteps}");
            }
        }

        private void drawClearWorld(Graphics g)
        {
            g.Clear(Color.Black);
            Pen p = new Pen(Brushes.White, CellBorderWidth);
            for (int i = 0; i <= WSizeX; i++)
                g.DrawLine(p, i * CellSide, 0, i * CellSide, CellSide * WSizeY);
            for (int i = 0; i <= WSizeY; i++)
                g.DrawLine(p, 0, i * CellSide, WSizeX * CellSide, i * CellSide);
        }

        private void drawEntities(Graphics g)
        {
            var w = ss.World as World;
            for(int i = 0; i < ss.World.SizeX; i++) 
            { 
                for(int j = 0; j < ss.World.SizeY; j++)
                {
                    var e = w.GetEntityIn((uint)i, (uint)j);
                    int c1 = 2, c2 = 3;
                    var rec = new Rectangle(CellSide * i + c1, CellSide * j + c1, CellSide - c2, CellSide - c2);

                    if (e.GetType().Equals(typeof(FoodEntity)))
                    {
                        g.FillRectangle(Brushes.Green, rec);
                    }
                    else if (e.GetType().Equals(typeof(PoisonEntity)))
                    {
                        g.FillRectangle(Brushes.Red, rec);
                    }
                    else if (e.GetType().Equals(typeof(BrickEntity)))
                    {
                        g.FillRectangle(Brushes.Gray, rec);
                    }
                    else if (e.GetType().Equals(typeof(Body)))
                    {
                        g.FillRectangle(Brushes.Blue, rec);
                    }
                    else if (e.GetType().Equals(typeof(EmptyEntity)))
                    {
                        g.FillRectangle(Brushes.Black, rec);
                    }
                }
            }
        }
    }
}
