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

		public Turret(Direction direction)
		{
			_direction = (int)direction;
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

			return new CreatureCommand(0,0);
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
			return GameObject.FromJsonObject(jsonObject);
		}
	}
}