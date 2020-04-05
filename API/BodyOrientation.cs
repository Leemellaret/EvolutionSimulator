using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSimulator.API
{
	public class BodyOrientation
	{
		public uint X { get; private set; }
		public uint Y { get; private set; }
		public WorldDirection Direction { get; private set; }

		public BodyOrientation(uint x, uint y, WorldDirection direction)
		{
			X = x;
			Y = y;
			Direction = direction;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (!GetType().Equals(obj.GetType()))
				return false;

			BodyOrientation cObj = (BodyOrientation)obj;
			return X == cObj.X && Y == cObj.Y && Direction == cObj.Direction;
		}
		public double DistanceTo(BodyOrientation other)
		{
			return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
		}
		public BodyOrientation MovePoint(uint dx, uint dy)
		{
			return new BodyOrientation(X + dx, Y + dy, Direction);
		}

		/// <summary>
		/// 0dim - сторона света к которой прибавляют значение. 1dim - какое значение прибавляют (0, 1, 2, 3)
		/// </summary>
		private static WorldDirection[,] rotationResults = new WorldDirection[,] { { WorldDirection.north, WorldDirection.west, WorldDirection.south, WorldDirection.east },
																				   { WorldDirection.east, WorldDirection.north, WorldDirection.west, WorldDirection.south },
																				   { WorldDirection.south, WorldDirection.east, WorldDirection.north, WorldDirection.west },
																				   { WorldDirection.west, WorldDirection.south, WorldDirection.east, WorldDirection.north } };
		public BodyOrientation RotateDirection(int value)
		{
			// 0 - не менять
			//<0 - повернуть против часовой
			//>0 - повернуть по часовой
			if(value < 0)
			{
				value += 4;
			}
			value %= 4;
			return new BodyOrientation(X, Y, rotationResults[(int)Direction, value]);
		}
	}
}
