using System.Windows;

namespace Digger.Architecture
{
	public enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}

	public static class DirectionHelper
	{
		public static Vector GetVec(Direction dir)
		{
			switch (dir)
			{
				case Direction.Up:return new Vector(0,-1);
				case Direction.Right:return new Vector(1,0);
				case Direction.Down:return new Vector(0,1);
				case Direction.Left:return new Vector(-1,0);
			}

			return new Vector(0,0);
		}
	}
}