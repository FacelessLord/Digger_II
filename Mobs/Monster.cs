using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Monster : ILiving
	{
		string
			_way = "left"; // вектор движения моба, нужно создать специальный класс вектора для "умного" движения мобов

		private int _blocksLeft = 0;

		public CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand {_deltaY = 0};
			if (_time % 3 == 0)
			{
				if ((_map[x + 1, y] == null || _map[x + 1, y] is Player) && _way == "left")
				{
					moving._deltaX++;
					_way = "left";

				}
				else _way = "right"; // - заменить

				if ((_map[x - 1, y] == null || _map[x - 1, y] is Player) && _way == "right")
				{
					moving._deltaX--;
					_way = "right";
				}
				else _way = "left"; // - заменить

				if (_map[x + moving._deltaX, y + moving._deltaY] is Monster ||
				    (_map[x + moving._deltaX, y] != null
				     && !(_map[x + moving._deltaX, y + moving._deltaY] is Gold)
				     && !(_map[x + moving._deltaX, y + moving._deltaY] is Monster)
				     && !(_map[x + moving._deltaX, y + moving._deltaY] is Player)))
				{
					moving._deltaX = 0;
				}

			}

			return moving;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return conflictedObject is Sack || conflictedObject is Monster || conflictedObject is FakeSack ||
			       conflictedObject is FireBlock||
			       conflictedObject is Key;
		}

		public int GetDrawingPriority()
		{
			return 11;
		}

		public string GetImageFileName()
		{
			return "Monster.png";
		}

		public bool IsSolidObject()
		{
			return false;
		}


		public bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}
	}
}