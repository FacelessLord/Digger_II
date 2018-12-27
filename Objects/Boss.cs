using System;
using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class Boss : Living
	{
		int _locationX;
		int _locationY;
		int _time = 0;
		private int _blocksLeft = 0;
		
        bool _isAlive = true;
		
		System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer() ;


		public override CreatureCommand Update(int x, int y)
		{

			_timer.Interval = 4;
			_timer.Tick +=SpawnMob;
            
			var moving = new CreatureCommand(0,0);
			_locationX = x + moving._deltaX;
			_locationY = y + moving._deltaY;
			if (_time % 10 == 0 && Game._map[x, y + 1] == null && Game._locX == x && _locationY < Game._locY && _isAlive)
			{

				Game._map[_locationX, _locationY + 1] = new FakeSack();
				_timer.Start();
			}
			return moving;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
            _isAlive = conflictedGameObject is Key || conflictedGameObject is Player || 
                     conflictedGameObject is Sack || conflictedGameObject is FakeSack;

            return _isAlive;
		}
		
		
		public override bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override int GetDrawingPriority()
		{
			return 4;
		}
		private void SpawnMob(object sender, EventArgs args)
		{
			_time++;
			if (Game._locX == _locationX && _locationY > Game._locY && _isAlive)
				Game._map[_locationX, _locationY + 1] = new FakeSack();
		}
		public override string GetImageFileName()
		{
			return "Boss.png";
		}
	}
}