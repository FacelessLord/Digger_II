using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class Gold : GameObject
	{
		public bool _isFalling;

		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0, 0);
		}

		public override bool CanBeDisplacedBy(GameObject obj)
		{
			return obj is Living;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			if (conflictedGameObject is Player)
				if (Game._time < 3600) //...
					Game._scores += 20;
				else
					Game._scores += 10;
			return true;
		}

		public override int GetDrawingPriority()
		{
			return 6;
		}

		public override string GetImageFileName()
		{
			return "Gold.png";
		}

		public override bool IsSolidObject()
		{
			return false;
		}
	}
}