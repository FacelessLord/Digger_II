using static Digger.Game;
using System;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class Boss : ILiving
	{
		int _locationX;
		int _locationY;
		int _time = 0;
		private int _blocksLeft = 0;
		
        bool _isAlive = true;
		
		System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer() ;


		public CreatureCommand Update(int x, int y)
		{

			_timer.Interval = 4;
			_timer.Tick +=SpawnMob;
            
			var moving = new CreatureCommand(0,0);
			_locationX = x + moving._deltaX;
			_locationY = y + moving._deltaY;
			if (_time % 10 == 0 && _map[x, y + 1] == null && _locX == x && _locationY < _locY && _isAlive)
			{

				_map[_locationX, _locationY + 1] = new FakeSack();
				_timer.Start();
			}
			return moving;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
            _isAlive = conflictedObject is Key || conflictedObject is Player || 
                     conflictedObject is Sack || conflictedObject is FakeSack;

            return _isAlive;
		}

		public bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}

		public bool IsSolidObject()
		{
			return false;
		}

		public int GetDrawingPriority()
		{
			return 4;
		}
		private void SpawnMob(object sender, EventArgs args)
		{
			_time++;
			if (_locX == _locationX && _locationY > _locY && _isAlive)
				_map[_locationX, _locationY + 1] = new FakeSack();
		}
		public string GetImageFileName()
		{
			return "Boss.png";
		}
	}
}