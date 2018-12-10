using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBlock : IObject
	{
		public CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand();
		}

		public string GetImageFileName()
		{
			return "Fireblock.png";
		}

		public int GetDrawingPriority()
		{
			return 6;
		}

		public bool IsSolidObject()
		{
			return false;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return !(conflictedObject is Player || conflictedObject is Monster);
		}
	}
}