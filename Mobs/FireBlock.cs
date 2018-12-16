using System.Net.Json;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBlock : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0);
		}

		public override string GetImageFileName()
		{
			return "Fireblock.png";
		}

		public override int GetDrawingPriority()
		{
			return 6;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return !(conflictedGameObject is Player || conflictedGameObject is Monster);
		}
	}
}