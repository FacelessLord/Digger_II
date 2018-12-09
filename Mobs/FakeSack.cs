using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class FakeSack : ICreature
	{
		int FreeFall;
		bool IsFalling;

		public CreatureCommand Act(int x, int y)
		{
			var moving = new CreatureCommand {DeltaY = 0};
			if (y + 1 < MapHeight && (Map[x, y + 1] == null
			                          || (FreeFall > 0 && (Map[x, y + 1] is Player
			                                               || Map[x, y + 1] is Wall
			                                               || Map[x, y + 1] is Monster) || IsFalling)))
			{
				moving.DeltaY++;
				FreeFall++;
				IsFalling = true;
			}

			if (FreeFall > 1 && !IsFalling)
				moving.TransformTo = new Gold();
			if (!IsFalling)
				FreeFall = 0;
			if (y + 1 < MapHeight && (Map[x, y + 1] is Terrain
			                          || Map[x, y + 1] is Sack))
				IsFalling = false;
			else IsFalling = false;
			return moving;
		}

		public bool DeadInConflict(ICreature conflictedObject)
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