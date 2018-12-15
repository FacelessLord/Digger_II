using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class FakeSack : IObject
	{
		int _freeFall;
		bool _isFalling;

		public CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0,0);
			if (y + 1 < MapHeight && (_map[x, y + 1] == null
			                          || (_freeFall > 0 && (_map[x, y + 1] is Player
			                                               || _map[x, y + 1] is Wall
			                                               || _map[x, y + 1] is Monster) || _isFalling)))
			{
				moving._deltaY++;
				_freeFall++;
				_isFalling = true;
			}

			if (_freeFall > 1 && !_isFalling)
				moving._transformTo = new Gold();
			if (!_isFalling)
				_freeFall = 0;
			if (y + 1 < MapHeight && (_map[x, y + 1] is Terrain
			                          || _map[x, y + 1] is Sack))
				_isFalling = false;
			else _isFalling = false;
			return moving;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return conflictedObject is Wall;
		}

		public int GetDrawingPriority()
		{
			return 6;
		}

		public string GetImageFileName()
		{
			return "FakeSack.png";
		}

		public bool IsSolidObject()
		{
			return true;
		}
	}
}