using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvolutionSimulator.API;

namespace EvolutionSimulator.foo52ruEvolution
{
    class Body : IBody, IEntity
    {
        public string Id { get; set; }
        private World world;
        public IWorld World { get => world; }
        public double Energy { get; private set; }
        public BodyOrientation Orientation { get; private set; }
        public double[] Data { get; private set; }
        public double InitData { get; private set; }

        public Body(string id, World world, double initEnergy, BodyOrientation pos, double initData)
        {
            Id = id;
            this.world = world;
            Energy = initEnergy;
            Orientation = pos;
            Data = new double[] { initData };
            InitData = initData;
        }

        private static (int, int)[] dc = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1) };

        public void InteractWithWorld(double[] commands)
        {
            int c = (int)commands[0];
            if (c >= 24)
                throw new ArgumentException($"Эту команду {c} тело распознать не может.");

            uint nx = Orientation.X + (uint)dc[c % 8].Item1;
            uint ny = Orientation.Y + (uint)dc[c % 8].Item2;
            IEntity e = world.GetEntityIn(nx, ny);

            if (e.GetType().Equals(typeof(PoisonEntity)))
                Data[0] = 1;
            else if (e.GetType().Equals(typeof(BrickEntity)))
                Data[0] = 2;
            else if (e.GetType().Equals(typeof(Body)))
                Data[0] = 3;
            else if (e.GetType().Equals(typeof(FoodEntity)))
                Data[0] = 4;
            else if (e.GetType().Equals(typeof(EmptyEntity)))
                Data[0] = 5;
            else
                throw new InvalidOperationException($"В ячейке [{nx},{ny}] нераспознанный объект.");

            if (c < 8)
            {
                if (e.GetType().Equals(typeof(FoodEntity)))
                {
                    Energy += (e as FoodEntity).Value - 1;
                    world.Cells[Orientation.X, Orientation.Y].Entity = new EmptyEntity();
                    Orientation = new BodyOrientation(nx, ny, WorldDirection.all);
                    world.Cells[nx, ny].Entity = this;
                }
                else if (e.GetType().Equals(typeof(PoisonEntity)))
                {
                    Die();
                    world.Cells[Orientation.X, Orientation.Y].Entity = new PoisonEntity((uint)Energy);
                }
                else if (e.GetType().Equals(typeof(BrickEntity)) || e.GetType().Equals(typeof(Body)))
                {
                    Energy -= 1;
                }
                else if (e.GetType().Equals(typeof(EmptyEntity)))
                {
                    Energy -= 1;
                    world.Cells[Orientation.X, Orientation.Y].Entity = new EmptyEntity();
                    Orientation = new BodyOrientation(nx, ny, WorldDirection.all);
                    world.Cells[nx, ny].Entity = this;
                }

            }
            else if (c < 16)
            {
                if (e.GetType().Equals(typeof(FoodEntity)))
                {
                    Energy += (e as FoodEntity).Value - 1;
                    world.Cells[nx, ny].Entity = new EmptyEntity();
                }
                else if (e.GetType().Equals(typeof(PoisonEntity)))
                {
                    world.Cells[nx, ny].Entity = new FoodEntity((e as PoisonEntity).Value);
                    Energy -= 1;
                }
                else if (e.GetType().Equals(typeof(BrickEntity)) || e.GetType().Equals(typeof(Body)) || e.GetType().Equals(typeof(EmptyEntity)))
                {
                    Energy -= 1;
                }
            }
            else if (c < 24)
            {
                Energy -= 1;
            }
        }

        public void Die()
        {
            world.RemoveBody(this);
            /*world.Cells[Orientation.X, Orientation.Y].Entity = new EmptyEntity();
            world.Bodies.Remove(this);*/
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (!obj.GetType().Equals(this.GetType()))
                return false;

            return (obj as Body).Id == Id;

        }

        public override string ToString()
        {
            return Id;
        }
    }
}
