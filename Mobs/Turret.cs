using Digger.Architecture;
using static Digger.Game;
using System;

namespace Digger.Mobs
{
	public class Turret : IObject
	{
		private int _timer = 0;
		private int _speed = 2;
		public int _direction = 3;

		public Turret(Direction direction)
		{
			_direction = (int)direction;
		}
		public Turret(int direction)
		{
			_direction = direction;
		}
		public CreatureCommand Update(int x, int y)
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
		
		public string GetImageFileName()
		{
			return "Turret.png";
		}

		public int GetDrawingPriority()
		{
			return 0;
		}

		public bool IsSolidObject()
		{
			return true;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			return false;
		}
	}
}