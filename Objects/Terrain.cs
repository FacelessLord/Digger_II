using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	class Terrain : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0, 0, this);
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return true;
		}

		public override bool CanBeDisplacedBy(GameObject obj)
		{
			return obj is Player;
		}

		public override int GetDrawingPriority()
		{
			return 8;
		}

		public override string GetImageFileName()
		{
			return "Terrain.png";
		}

		public override bool IsSolidObject()
		{
			return true;
		}
	}
}