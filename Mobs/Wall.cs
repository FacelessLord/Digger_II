using Digger.Architecture;

namespace Digger.Mobs
{
	public class Wall : IObject
	{
		public CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand {_deltaX = 0, _deltaY = 0};
		}

		public bool DestroyedInConflict(IObject conflictedObject)
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