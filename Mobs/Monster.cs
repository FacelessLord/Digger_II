using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Monster : ICreature
	{
		string way = "left"; // вектор движения моба, нужно создать специальный класс вектора для "умного" движения мобов
		public CreatureCommand Act(int x, int y)
		{
			var moving = new CreatureCommand { DeltaY = 0 };
			if (time % 6 == 0)
			{
				if ((Map[x + 1, y] == null || Map[x + 1, y] is Player) && way == "left")
				{
					moving.DeltaX++;
					way = "left";
                    
				}
				else way = "right"; // - заменить
                
				if ((Map[x - 1, y] == null || Map[x - 1, y] is Player) && way == "right")
				{
					moving.DeltaX--;
					way = "right";
				}
				else way = "left"; // - заменить

				if (Map[x + moving.DeltaX, y + moving.DeltaY] is Monster ||
				    (Map[x + moving.DeltaX, y] != null
				     && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Gold)
				     && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Monster)
				     && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Player)))
				{
					moving.DeltaX = 0;
				}
                
			}

			return moving;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{ 
			return conflictedObject is Sack || conflictedObject is Monster || conflictedObject is Sack;
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
	}
}