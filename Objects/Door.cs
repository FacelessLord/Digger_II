using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class Door : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0);
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			if (conflictedGameObject is Key)
			{
				Game._scores += 50;
			}

			return conflictedGameObject is Key;
		}

		public override bool CanBeDisplacedBy(GameObject obj)
		{
			return obj is Key;
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override string GetImageFileName()
		{
			//if (false) return "OpenDoor.png";
			return "DoorClose.png";
		}

		public override bool IsSolidObject()
		{
			return true;
		}
	}
}