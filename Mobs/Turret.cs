using Digger.Architecture;
using static Digger.Game;
using System;
using System.Net.Json;

namespace Digger.Mobs
{
	public class Turret : GameObject
	{
		private int _timer = 0;
		private int _speed = 3;
		public int _direction = 3;

		public Turret()
		{
		}

		public Turret(int direction)
		{
			_direction = direction;
		}

		public override CreatureCommand Update(int x, int y)
		{
			_timer++;
			if (_timer % _speed == 0)
			{
				var vec = DirectionHelper.GetVec(_direction);
				var ballRequest = new SpawnRequest(new FireBall(_direction), (int) (x + vec.X), (int) (y + vec.Y));
				Game.RequestSpawn(ballRequest);
			}

			return new CreatureCommand(0, 0);
		}

		public override string GetImageFileName()
		{
			return "Turret.png";
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override bool IsSolidObject()
		{
			return true;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject)
		{
			return false;
		}

		public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
		{
			var po = GameObject.FromJsonObject(jsonObject);
			var dir = 3;
			foreach (var obj in jsonObject)
			{
				if (obj is JsonNumericValue n)
				{
					if (n.Name == "direction")
					{
						dir = (int) n.Value;
					}
				}
			}

			((Turret) po._obj)._direction = dir;
			return po;
		}
	}
}