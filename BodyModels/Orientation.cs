using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.BodyModels
{
	public class Orientation
	{
		public uint X { get; private set; }
		public uint Y { get; private set; }
		public WorldDirection Direction { get; private set; }

		public Orientation(uint x, uint y, WorldDirection direction)
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
		public Orientation MovePoint(uint dx, uint dy)
		{
			return new Orientation(X + dx, Y + dy, Direction);
		}

		/// <summary>
		/// 0dim - сторона света к которой прибавляют значение. 1dim - какое значение прибавляют (0, 1, 2, 3)
		/// </summary>
		private static WorldDirection[,] rotationResults = new WorldDirection[,] { { WorldDirection.north, WorldDirection.west, WorldDirection.south, WorldDirection.east },
																				   { WorldDirection.east, WorldDirection.north, WorldDirection.west, WorldDirection.south },
																				   { WorldDirection.south, WorldDirection.east, WorldDirection.north, WorldDirection.west },
																				   { WorldDirection.west, WorldDirection.south, WorldDirection.east, WorldDirection.north } };
		public Orientation RotateDirection(int value)
		{
			// 0 - не менять
			//<0 - повернуть против часовой
			//>0 - повернуть по часовой
			if(value < 0)
			{
				value += 4;
			}
			value %= 4;
			return new Orientation(X, Y, rotationResults[(int)Direction, value]);
		}
	}
}
