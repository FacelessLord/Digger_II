using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class FakeSack : GameObject
	{
		int _freeFall;
		bool _isFalling;

		public override CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0,0);
			if (y + 1 < Game.MapHeight && (Game._map[x, y + 1] == null
			                          || (_freeFall > 0 && (Game._map[x, y + 1] is Player
			                                               || Game._map[x, y + 1] is Wall
			                                               || Game._map[x, y + 1] is Monster) || _isFalling)))
			{
				moving._deltaY++;
				_freeFall++;
				_isFalling = true;
			}

			if (_freeFall > 1 && !_isFalling)
				moving._transformTo = new Gold();
			if (!_isFalling)
				_freeFall = 0;
			if (y + 1 < Game.MapHeight && (Game._map[x, y + 1] is Terrain
			                          || Game._map[x, y + 1] is Sack))
				_isFalling = false;
			else _isFalling = false;
			return moving;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return conflictedGameObject is Wall;
		}

		public override int GetDrawingPriority()
		{
			return 6;
		}

		public override string GetImageFileName()
		{
			return "FakeSack.png";
		}

		public override bool IsSolidObject()
		{
			return true;
		}
	}
}