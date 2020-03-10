using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels
{
	public class Orientation
	{
		public int X { get; private set; }
		public int Y { get; private set; } 
		public WorldDirection Direction { get; private set; }

		public Orientation(int x, int y, WorldDirection direction)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (!GetType().Equals(obj.GetType()))
				return false;

			Orientation cObj = (Orientation)obj;
			return X == cObj.X && Y == cObj.Y && Direction == cObj.Direction;
		}
		public double DistanceTo(Orientation other)
		{
			return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
		}
		public Orientation MovePoint(int dx, int dy)
		{
			return new Orientation(X + dx, Y + dy, Direction);
		}
		public Orientation RotateDirection(int value)
		{
			// 0 - не менять
			//<0 - повернуть против часовой
			//>0 - повернуть по часовой
			int v = value > 0 ? 1 : (value < 0 ? -1 : 0);
			return new Orientation(X, Y, (WorldDirection)(((int)Direction + v) % 4));
		}
	}
}
