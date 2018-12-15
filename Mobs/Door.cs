using System.Net.Json;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Door : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0);
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject)
		{
			if (conflictedGameObject is Key)
			{
				_scores += 50;
			}

			return conflictedGameObject is Key;
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
		
		public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			return GameObject.FromJsonObject(jsonObject);
		}
	}
}