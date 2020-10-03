using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using EvolutionSimulator.Run;
using System.Text.Json;
using System.IO;
using System.Drawing.Text;

namespace EvolutionSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.OpenLogFile(@"e:\ES\1.txt");

            int wSizeX = 100, wSizeY = 100;
            Cell[,] cells = new Cell[wSizeX, wSizeY];
            int countOfWorldSteps = 100;
            int foodIncrease = 14;
            int initFoodCount = foodIncrease * 50;

            for (int i = 0; i < wSizeX; i++)
            {
                for (int j = 0; j < wSizeY; j++)
                {
                    cells[i, j] = new Cell(initFoodCount, foodIncrease);
                }
            }
            Evolution evolution = new Evolution();
            World world = new World(evolution, cells);

            var firstPopulation = evolution.MakeFirstPopulation();
            world.AddCreature(firstPopulation[0], new Run.Orientation(wSizeX - 1, wSizeY - 1));
            world.AddCreature(firstPopulation[1], new Run.Orientation(0, 0));

            #region Запись в файл информации о мире
            //Logger.Log($"World parameters:");
            //Logger.Log($"World size X={wSizeX}");
            //Logger.Log($"World size Y={wSizeY}");
            //for (int i = 0; i < wSizeX; i++)
            //{
            //    for (int j = 0; j < wSizeY; j++)
            //    {
            //        Logger.Log($"Cell[{i}, {j}]=(Food={cells[i, j].FoodCount}, FoodIncrease={cells[i, j].FoodIncrease})");
            //    }
            //}
            //Logger.Log($"\nCreatureParameters constants:");
            //Logger.Log($"Alpha1={CreatureParameters.alpha1}");
            //Logger.Log($"MaxHealth(1)={CreatureParameters.MaxHealth(1)}");
            //Logger.Log($"h1(1)={CreatureParameters.h1(1)}");
            //Logger.Log($"RegenerationValue(1)={CreatureParameters.RegenerationValue(1)}");
            //Logger.Log($"InitEnergy(1)={CreatureParameters.InitEnergy(1)}");
            //Logger.Log($"e1(1)={CreatureParameters.e1(1)}");
            //Logger.Log($"e2(1)={CreatureParameters.e2(1)}");
            //Logger.Log($"AbsorbAble(1)={CreatureParameters.AbsorbAble(1)}");
            //Logger.Log($"HitForce(1)={CreatureParameters.HitForce(1)}");
            //Logger.Log("");
            //Logger.Log($"First Population:\n{world.StatesOfCreatures()}");
            //Logger.Log("");
            #endregion

            string[] chartAreaNames = new string[] 
            {
                "All Food", 
                "Absorbtion of food", 
                "Creature populations"
                /*, "Death info", "Uniformity of distribution" */
            };
            List<SeriesSettings> series = new List<SeriesSettings>() 
            {
                new SeriesSettings("All food", chartAreaNames[0], Color.Brown, countOfWorldSteps),
                new SeriesSettings("Total absorbtion of food", chartAreaNames[1], Color.Blue, countOfWorldSteps),
                new SeriesSettings($"Population of {(int)evolution.Alphas[0]}-Alpha", chartAreaNames[2], Color.Red, countOfWorldSteps),
                new SeriesSettings($"Population of {(int)evolution.Alphas[1]}-Alpha", chartAreaNames[2], Color.Green, countOfWorldSteps)/*,
                new SeriesSettings("Deaths in fight", chartAreaNames[3], Color.DarkRed, countOfWorldSteps),
                new SeriesSettings("Deaths from starvation", chartAreaNames[3], Color.DarkGray, countOfWorldSteps),
                new SeriesSettings("Uniformity", chartAreaNames[4], Color.DarkGoldenrod, countOfWorldSteps)*/
            };

            for (int i = 1; i <= countOfWorldSteps; i++)
            {
                AddData(world, series);
                //Console.WriteLine($"{i} TF={totalFood:.00} TAoF={totalFoodConsumption:.00} S{(int)evolution.Alphas[0]}C={alpha0:0000} S{(int)evolution.Alphas[1]}C={alpha1:0000}");
                //Console.WriteLine(i);

                // alpha2Count={alpha2:0000} alpha8Count={alpha8:0000} Count of: Eat={world.CountOfActions[(int)CreatureAction.Eat]} Go={world.CountOfActions[(int)CreatureAction.Go]} Hit={world.CountOfActions[(int)CreatureAction.Hit]} Repr={world.CountOfActions[(int)CreatureAction.Reproduce]}
                //Logger.Log($"World step={i:0000000}");
                world.MakeInteractions();
            }
            AddData(world, series);
            //Logger.Log($"Count of: Eat={world.CountOfActions[(int)CreatureAction.Eat]} Go={world.CountOfActions[(int)CreatureAction.Go]} Hit={world.CountOfActions[(int)CreatureAction.Hit]} Repr={world.CountOfActions[(int)CreatureAction.Reproduce]}");
            File.WriteAllText($"e:\\es\\results of foodIncrease {foodIncrease}.txt", JsonSerializer.Serialize(series));
            Console.WriteLine($"food increase = {foodIncrease} done.");

            ChartForm chartForm = new ChartForm(chartAreaNames, series);
            chartForm.ShowDialog();

            Logger.CloseLogFile();
        }

        static void AddData(World world, List<SeriesSettings> series)
        {
            int alpha0 = world.CountOfCreaturesWithAlpha(world.Evolution.Alphas[0]);
            int alpha1 = world.CountOfCreatures - alpha0;
            double totalFood = world.TotalFood;
            double totalFoodConsumption = world.TotalFoodAbsorbed;
            series[0].AddPoint(totalFood);
            series[1].AddPoint(totalFoodConsumption);
            series[2].AddPoint(alpha0);
            series[3].AddPoint(alpha1);
            /*series[4].AddPoint(World.Deaths[0]);
            series[5].AddPoint(World.Deaths[1]);
            series[6].AddPoint(world.UniformityOfDistribution());*/
        }
    }

    class ChartForm : Form
    {
        private Chart chart;
        private static System.Globalization.NumberFormatInfo numberFormatInfo = new System.Globalization.CultureInfo("en-Us", false).NumberFormat;
        public ChartForm(string[] chartAreaNames, List<SeriesSettings> seriesSettings)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Визуализация информации в графиках";
            this.SizeChanged += FormSizeChanged;

            /*FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.BackColor = Color.LightBlue;
            panel.Parent = this;
            panel.Dock = DockStyle.Fill;
            panel.FlowDirection = FlowDirection.TopDown;
            panel.WrapContents = false;
            panel.AutoScroll = true;*/

            chart = new Chart();
            //chart.Parent = panel;
            chart.Parent = this;
            chart.Dock = DockStyle.Fill;
            chart.MouseMove += mouseMoveInChart;
            /*chart.Width = Width;
            chart.Height = Height;*/




            string firstChartAreaName = chartAreaNames[0];
            float h = 100 / chartAreaNames.Length;
            int i = 0;
            foreach (var chartAreaName in chartAreaNames)
            {
                var chartArea = new ChartArea(chartAreaName);
                chartArea.AlignWithChartArea = firstChartAreaName;
                chartArea.AlignmentOrientation = AreaAlignmentOrientations.Vertical;
                chartArea.AlignmentStyle = AreaAlignmentStyles.All;
                chartArea.Position.Auto = false;
                chartArea.Position.X = 0;
                chartArea.Position.Y = h * (i++);
                chartArea.Position.Width = 92;
                chartArea.Position.Height = h;

                chartArea.AxisX.Minimum = 0;
                chartArea.AxisY.Minimum = 0;
                chartArea.AxisX.Title = "Time";
                chartArea.AxisY.Title = "Count";

                chartArea.CursorX.Interval = 1;
                chartArea.CursorX.LineWidth = 2;
                chartArea.CursorX.LineColor = Color.Black;
                /*chartArea.CursorX.LineWidth = 3;
                chartArea.CursorY.LineWidth = 3;
                chartArea.CursorX.LineColor = Color.Black;
                chartArea.CursorY.LineColor = Color.Black;
                chartArea.AxisX.ScaleView.Zoomable = true;
                chartArea.AxisY.ScaleView.Zoomable = true;
                chartArea.CursorX.Interval = 1;
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.CursorY.Interval = 1;
                chartArea.CursorY.IsUserEnabled = true;
                chartArea.CursorY.IsUserSelectionEnabled = true;*/

                chart.ChartAreas.Add(chartArea);
            }

            List <Series> series = new List<Series>(seriesSettings.Count);
            foreach(var serSet in seriesSettings)
            {
                Series s = new Series(serSet.Name);
                

                Legend sLegend = new Legend("Legend of " + serSet.Name);
                sLegend.DockedToChartArea = serSet.ChartAreaName;
                sLegend.IsDockedInsideChartArea = true;
                sLegend.Docking = Docking.Right;
                chart.Legends.Add(sLegend);
                s.Legend = sLegend.Name;
                s.IsVisibleInLegend = true;

                s.ChartType = SeriesChartType.Line;
                s.ChartArea = serSet.ChartAreaName;
                s.Color = serSet.Color;
                s.BorderWidth = serSet.BorderWidth;

                var points = serSet.Points;
                for (int t = 0; t < points.Count; t++)
                {
                    s.Points.AddXY(t, points[t]);
                }

                chart.Series.Add(s);
            }
        }

        ToolTip tooltip = new ToolTip();
        private void mouseMoveInChart(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            chart.ChartAreas[0].CursorX.SetCursorPixelPosition(pos, true);

            var t = chart.ChartAreas[0].CursorX.Position;
            string res = $"t={((int)t).ToString("N", numberFormatInfo)}";
            foreach (var s in chart.Series)
            {
                if (!double.IsNaN(t) && 0 <= t && t < s.Points.Count)
                {
                    res += "\n";
                    double val = s.Points[(int)t].YValues[0];
                    res += $"{s.Name} = {val.ToString("N", numberFormatInfo)}";
                    if (t > 0)
                        res += $"   DELTA={(s.Points[(int)t].YValues[0] - s.Points[(int)t - 1].YValues[0]).ToString("N", numberFormatInfo)}";
                }
            }
            tooltip.Show(res, chart, pos.X -100, pos.Y);
        }

        private void FormSizeChanged(object sender, EventArgs e)
        {
            chart.Size = new Size(Width, Height);
            //oneChartBoxHeight = Height / 2;
        }
    }
    [Serializable]
    class SeriesSettings
    {
        public string Name { get; }
        public string ChartAreaName { get; }
        public List<double> Points { get; }
        public Color Color { get; }
        public int BorderWidth { get; }

        public SeriesSettings(string name, string chartAreaName, Color color, int countOfPoints,  int borderWidth = 2)
        {
            Name = name;
            ChartAreaName = chartAreaName;
            Color = color;
            Points = new List<double>(countOfPoints);
            BorderWidth = borderWidth;
        }

        public void AddPoint(double y)
        {
            Points.Add(y);
        }
    }
}