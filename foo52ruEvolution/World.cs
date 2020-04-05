using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    class World : IWorld
    {
        private List<Body> bodies;
        private List<IBody> bodiesAsIBody;
        public Cell[,] Cells { get; private set; }

        public uint SizeX { get; private set; }
        public uint SizeY { get; private set; }

        public World(uint sizeX, uint sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            bodies = new List<Body>();
            bodiesAsIBody = new List<IBody>();

            Cells = new Cell[sizeX, sizeY];
            MakeEmpty();

            Random r = new Random();
            int countOfBricks = (int)Math.Sqrt(sizeX * sizeY);
            for(int i = 0; i < countOfBricks; i++)
            {
                Cells[r.Next((int)sizeX), r.Next((int)sizeY)].Entity = new BrickEntity();
            }
        }

        public void AddBody(IBody body)
        {
            if (bodies.Find(x => x.Id == (body as Body).Id) != null)
                throw new ArgumentException($"Тело с таким ID {(body as Body).Id} уже существует", "newBody");

            if (!Cells[body.Orientation.X, body.Orientation.Y].Entity.GetType().Equals(typeof(EmptyEntity)))
                throw new ArgumentException("На клетке, куда нужно поставить данное существо, уже есть другая сущность", "newBody");
            
            Cells[body.Orientation.X, body.Orientation.Y].Entity = body as Body;
            bodies.Add(body as Body);
            bodiesAsIBody.Add(body);
        }

        public void RemoveBody(IBody body)
        {
            if (bodies.Find(x => x.Id == (body as Body).Id) == null)
                throw new ArgumentException($"Тело с таким ID {(body as Body).Id} не существует", "newBody");

            if (!Cells[body.Orientation.X, body.Orientation.Y].Entity.GetType().Equals(typeof(Body)))
                throw new InvalidOperationException("По координатам данного тела в мире нет тела");

            if (!Cells[body.Orientation.X, body.Orientation.Y].Entity.Equals(body))
                throw new InvalidOperationException("По координатам данного тела в мире стоит тело, отличное от поданного");

            bodies.Remove(body as Body);
            bodiesAsIBody.Remove(body);
            Cells[body.Orientation.X, body.Orientation.Y].Entity = new EmptyEntity();
        }

        public IEntity GetEntityIn(uint x, uint y)
        {
            if (0 <= x && x < SizeX && 0 <= y && y < SizeY)
                return Cells[x, y].Entity;
            else
                return new BrickEntity();
        }

        public void MakeEmpty()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    Cells[i, j] = new Cell(new EmptyEntity());
                }
            }
        }

        public void Update()
        {
            Random r = new Random();
            int ci = r.Next((int)Math.Sqrt(Cells.Length)) * (r.Next(2));
            //ci = ci / 2 + ci / 3;
            for (int i = 0; i < ci; i++)
            {
                uint x = (uint)r.Next((int)SizeX), y = (uint)r.Next((int)SizeY);

                if (r.Next(10) == 0)
                {
                    if (GetEntityIn(x, y).GetType().Equals(typeof(EmptyEntity)))
                        Cells[x, y].Entity = new PoisonEntity(20);
                    else if (GetEntityIn(x, y).GetType().Equals(typeof(PoisonEntity)))
                        Cells[x, y].Entity = new PoisonEntity((Cells[x, y].Entity as PoisonEntity).Value + 20);
                }
                else
                {
                    if (GetEntityIn(x, y).GetType().Equals(typeof(EmptyEntity)))
                        Cells[x, y].Entity = new FoodEntity(20);
                    else if (GetEntityIn(x, y).GetType().Equals(typeof(FoodEntity)))
                        Cells[x, y].Entity = new FoodEntity((Cells[x, y].Entity as FoodEntity).Value + 20);
                }
            }
        }
        public void LeaveOnlyBricksAndBodies()
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (!Cells[i, j].Entity.GetType().Equals(typeof(BrickEntity)) && !Cells[i, j].Entity.GetType().Equals(typeof(Body)))
                        Cells[i, j] = new Cell(new EmptyEntity());
                }
            }
        }

        public void PrepareForNewGeneration()
        {
            LeaveOnlyBricksAndBodies();
        }

        public List<IBody> Bodies { get => bodiesAsIBody; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (!obj.GetType().Equals(this.GetType()))
                return false;

            var c = obj as World;
            if (c.SizeX == this.SizeX && c.SizeY == this.SizeY && bodies.SequenceEqual(c.bodies))
            {
                for (int i = 0; i < SizeX; i++)
                {
                    for (int j = 0; j < SizeY; j++)
                    {
                        if (!Cells[i, j].Equals(c.Cells[i, j]))
                            return false;
                    }
                }
                return true;
            }
            else 
                return false;
        }
    }
}
