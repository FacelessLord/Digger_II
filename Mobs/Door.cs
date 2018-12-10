using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Door : IObject
	{
		public CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand { };
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			if (conflictedObject is Key)
			{
				_scores += 50;
			}

			return conflictedObject is Key;
		}

		public int GetDrawingPriority()
		{
			return 5;
		}

		public string GetImageFileName()
		{
			//if (false) return "OpenDoor.png";
			return "DoorClose.png";
		}

		public bool IsSolidObject()
		{
			return true;
		}
	}
}