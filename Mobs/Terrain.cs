using Digger.Architecture;

namespace Digger.Mobs
{
	class Terrain : IObject
	{
		public CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0, 0, this);
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return true;
		}

		public int GetDrawingPriority()
		{
			return 8;
		}

		public string GetImageFileName()
		{
			return "Terrain.png";
		}

		public bool IsSolidObject()
		{
			return false;
		}
	}
}