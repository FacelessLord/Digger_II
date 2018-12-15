using Digger.Architecture;
using static Digger.Game;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Gold : IObject
	{
		public bool _isFalling;

		public CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0, 0);
			if (y + 1 < MapHeight && (_map[x, y + 1] == null
			                          || (_map[x, y + 1] is Player
			                              || _map[x, y + 1] is Monster) || _isFalling))
			{
				moving._deltaY++;
				_isFalling = true;
			}

			if (y + 1 == MapHeight && _map[x, 0] == null && _isFalling)
				moving._deltaY--;
			if (y + 1 < MapHeight && (_map[x, y + 1] is Terrain || _map[x, y + 1] is Sack) || _map[x, y + 1] is Gold)
				_isFalling = false;
			else _isFalling = false;

			return moving;
		}


		public bool DestroyedInConflict(IObject conflictedObject)
		{
			if (conflictedObject is Player)
				if (_time < 3600) //...
					_scores += 20;
				else
					_scores += 10;
			return true;
		}

		public int GetDrawingPriority()
		{
			return 6;
		}

		public string GetImageFileName()
		{
			return "Gold.png";
		}

		public bool IsSolidObject()
		{
			return false;
		}
	}
}