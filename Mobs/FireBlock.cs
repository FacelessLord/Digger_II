using System.Net.Json;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBlock : GameObject,IFieryObject
	{
		public bool _isBurningDown = true;
		
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0);
		}

		public override string GetImageFileName()
		{
			if (!_isBurningDown)
				return "ColdFireblock.png";
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
			if (!_isBurningDown)
			{
				Game.RequestSpawn(new SpawnRequest(this,coords[0],coords[1]));
			}
			return !(conflictedGameObject is Player || conflictedGameObject is Monster);
		}

		public static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			var prepObj = GameObject.FromJsonObject(jsonObject);
			var burn = false;
			foreach (var obj in jsonObject)
			{
				if (obj is JsonLiteralValue b)
				{
					if (b.Name == "isBurningDown")
					{
						burn = b.Value.Equals(JsonAllowedLiteralValues.True);
					}
				}
			}

			((FireBlock) prepObj._obj)._isBurningDown = burn;
			return prepObj;
		}

		public bool DestroyedWhenCollideWith(GameObject obj)
		{
			return _isBurningDown;
		}
	}
}