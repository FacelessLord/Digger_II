using Digger.Architecture;

namespace Digger.Mobs
{
	public class Wall : ICreature
	{
		public CreatureCommand Act(int x, int y)
		{
			return new CreatureCommand {DeltaX = 0, DeltaY = 0};
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			return conflictedObject is Door;
		}

		public int GetDrawingPriority()
		{
			return 1;
		}

		public string GetImageFileName()
		{
			return "Wall.png";
		}

		public bool IsSolidObject()
		{
			return true;
		}
	}
}