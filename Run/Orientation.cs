using System;

namespace EvolutionSimulator.Run
{
	public class Orientation
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Orientation(int x, int y)
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
			return X == cObj.X && Y == cObj.Y;
		}
		public double DistanceTo(Orientation other)
		{
			return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
		}
		public Orientation MovePoint(int dx, int dy)
		{
			return new Orientation(X + dx, Y + dy);
		}

		public override string ToString()
		{
			return $"[x={X} y={Y}]";
		}
	}
}
