using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Door : ICreature
	{
		public CreatureCommand Act(int x, int y)
		{
			return new CreatureCommand { };
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			if (conflictedObject is Key)
			{
				Scores += 50;
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