using System.Net.Json;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class Air : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			Game.RequestSpawn(new SpawnRequest(null,x,y,true));
			
			return new CreatureCommand(0,0,this);
		}

		public override string GetImageFileName()
		{
			return null;
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return true;
		}
	}
}