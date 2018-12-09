using static Digger.Game;

namespace Digger.Mobs
{
	public class Gold : ICreature
	{
		public CreatureCommand Act(int x, int y)
		{
			return new CreatureCommand();
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			if (conflictedObject is Player)
				if (time < 3600) //...
					Scores += 20;
				else
					Scores += 10;
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
	}
}