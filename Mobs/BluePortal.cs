using System.Net.Json;
using System.Windows;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class BluePortal : GameObject
	{
		public int _toX = 5;
		public int _toY = 5;
		
		public override CreatureCommand Update(int x, int y)
		{
			return new CreatureCommand(0,0,this);
		}

		public override string GetImageFileName()
		{
			return "BluePortal.png";
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
			var request = new SpawnRequest(conflictedGameObject,_toX,_toY).SetSearchMethod((x, y) =>
			{
				int tx = x;
				int ty = y;
				while (Game._map[tx, ty] != null)
				{
					tx++;
				}
				return new Vector(tx,ty);
			});
			Game.RequestSpawn(request);
			Game.RequestSpawn(new SpawnRequest(null,coords[0],coords[1],true));
			Game.RequestSpawn(new SpawnRequest(this,coords[0],coords[1]));
			
			return true;
		}
		
		public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			var po = GameObject.FromJsonObject(jsonObject);
			foreach (var obj in jsonObject)
			{
				if (obj is JsonNumericValue n)
				{
					if (n.Name == "toX")
					{
						((BluePortal)po._obj)._toX = (int) n.Value;
					}
					if (n.Name == "toY")
					{
						((BluePortal)po._obj)._toY = (int) n.Value;
					}
				}
			}
			return po;
		}
		
	}
}