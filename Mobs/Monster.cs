using System.Net.Json;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Monster : Living
	{
		string
			_way = "left"; // вектор движения моба, нужно создать специальный класс вектора для "умного" движения мобов

		private int _blocksLeft = 0;

		public override CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0, 0);
			if (_time % 3 == 0)
			{
				if ((_map[x + 1, y] == null || !_map[x + 1, y].IsSolidObject()) && _way == "left")
				{
					moving._deltaX++;
					_way = "left";

				}
				else _way = "right"; // - заменить

				if ((_map[x - 1, y] == null || !_map[x - 1, y].IsSolidObject()) && _way == "right")
				{
					moving._deltaX--;
					_way = "right";
				}
				else _way = "left"; // - заменить

				if
				(_map[x + moving._deltaX, y] != null
				 && (_map[x + moving._deltaX, y + moving._deltaY].IsSolidObject()))
				{
					moving._deltaX = 0;
				}

			}

			return moving;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return conflictedGameObject is Sack || conflictedGameObject is Monster ||
			       conflictedGameObject is FakeSack ||
			       conflictedGameObject is FireBlock || conflictedGameObject is FireBall ||
			       conflictedGameObject is Key || conflictedGameObject.IsSolidObject();
		}

		public override int GetDrawingPriority()
		{
			return 11;
		}

		public override string GetImageFileName()
		{
			return "Murloc.png";
		}

		public override bool IsSolidObject()
		{
			return false;
		}


		public override bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}
	}
}