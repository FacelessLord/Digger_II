using System.Net.Json;
using Digger.Architecture;

namespace Digger.Mobs
{
	class Terrain : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0, 0, this);
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject)
		{
			return true;
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
			return false;
		}

		public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			return GameObject.FromJsonObject(jsonObject);
		}
	}
}