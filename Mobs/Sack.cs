using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Sack : ICreature
	{
		public int FreeFall; //количетво клеток свободного падения 
		public bool IsFalling;

		public int GetDrawingPriority()
		{
			return 8;
		}

		public string GetImageFileName()
		{
			return "Sack.png";
		}

		public CreatureCommand Act(int x, int y)
		{

			var moving = new CreatureCommand { };
			if (y + 1 < MapHeight && (Map[x, y + 1] == null
			                          || (FreeFall > 0 && (Map[x, y + 1] is Player
			                                               || Map[x, y + 1] is Monster) || IsFalling)))
			{
				moving.DeltaY++;
				FreeFall++;
				IsFalling = true;
			}

			if (y + 1 == MapHeight && Map[x, 0] == null && IsFalling)
				moving.DeltaY--;
			if (FreeFall > 1 && !IsFalling)
				moving.TransformTo = new Gold();
			if (!IsFalling)
				FreeFall = 0;
			if (y + 1 < MapHeight && (Map[x, y + 1] is Terrain || Map[x, y + 1] is Sack))
				IsFalling = false;
			else IsFalling = false;

			return moving;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			return false;
		}

		public bool IsSolidObject()
		{
			return true;
		}
	}
}