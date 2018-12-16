using System.Net.Json;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class Wall : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0);
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return conflictedGameObject is Door;
		}

		public override int GetDrawingPriority()
		{
			return 1;
		}

		public override string GetImageFileName()
		{
			return "Wall.png";
		}

		public override bool IsSolidObject()
		{
			return true;
		}
	}
}