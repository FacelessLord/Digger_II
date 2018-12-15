using Digger.Architecture;
using static Digger.Game;
using System;

namespace Digger.Mobs
{
	public class Turret : IObject
	{
		private int _x = 0;
		private int _y = 0;
		System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

		public Turret()
		{
			_timer.Interval = 1;
			_timer.Tick +=SpawnMob;
		}
		
		public CreatureCommand Update(int x, int y)
		{
			_x = x;
			_y = y;
			return new CreatureCommand(0,0);
		}
		private void SpawnMob(object sender, EventArgs args)
		{
			_map[_x-1, _y] = new FireBall();
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