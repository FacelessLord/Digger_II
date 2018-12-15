using System.Windows;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBall : IObject
	{
		public Vector _direction;
		public FireBall()
		{
			_direction = new Vector(-1,0);
		}
		public FireBall(Direction direction)
		{
			_direction = DirectionHelper.GetVec(direction);
		}
		
		public CreatureCommand Update(int x, int y)
		{
			int dx = (int) _direction.X;
			int dy = (int) _direction.Y;
			if (Game._map[x + dx, y + dy] == null || !Game._map[x + dx, y + dy].IsSolidObject())
			{
				CreatureCommand cc = new CreatureCommand(dx,dy,this);
				return cc;
			}

			Game._map[x + dx, y + dy] = null;
			return new CreatureCommand(0,0,new FireBlock());
		}

		public string GetImageFileName()
		{
			return "Fireball.png";
		}

		public int GetDrawingPriority()
		{
			return 0;
		}

		public bool IsSolidObject()
		{
			return false;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return !(conflictedObject is FireBlock) && conflictedObject.IsSolidObject();
		}
	}
}