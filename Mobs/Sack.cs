using System.Net.Json;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Sack : GameObject
	{
		public int _freeFall; //количетво клеток свободного падения 
		public bool _isFalling;

		public override int GetDrawingPriority()
		{
			return 8;
		}

		public override string GetImageFileName()
		{
			return "Sack.png";
		}

		public override CreatureCommand Update(int x, int y)
		{

			var moving = new CreatureCommand(0,0);
			if (y + 1 < MapHeight && (_map[x, y + 1] == null
			                          || (_freeFall > 0 && (_map[x, y + 1] is Player
			                                               || _map[x, y + 1] is Monster) || _isFalling)))
			{
				moving._deltaY++;
				_freeFall++;
				_isFalling = true;
			}

			if (y + 1 == MapHeight && _map[x, 0] == null && _isFalling)
				moving._deltaY--;
			if (_freeFall > 1 && !_isFalling)
				moving._transformTo = new Gold();
			if (!_isFalling)
				_freeFall = 0;
			if (y + 1 < MapHeight && (_map[x, y + 1] is Terrain || _map[x, y + 1] is Sack))
				_isFalling = false;
			else _isFalling = false;

			return moving;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject)
		{
			return false;
		}

		public override bool IsSolidObject()
		{
			return true;
		}

		public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			return GameObject.FromJsonObject(jsonObject);
		}
	}
}