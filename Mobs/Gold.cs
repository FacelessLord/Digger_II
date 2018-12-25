using System.Net.Json;
using Digger.Architecture;
using static Digger.Game;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Gold : GameObject
	{
		public bool _isFalling;

		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0, 0);
		}


		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			if (conflictedGameObject is Player)
				if (_time < 3600) //...
					_scores += 20;
				else
					_scores += 10;
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