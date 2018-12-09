using static Digger.Game;
using System;

namespace Digger.Mobs
{
	public class Boss : ICreature
	{
		int locationX;
		int locationY;
		int time = 0; 
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() ;


		public CreatureCommand Act(int x, int y)
		{

			timer.Interval = 1;
			timer.Tick +=SpawnMob;
            
			var moving = new CreatureCommand { };
			locationX = x + moving.DeltaX;
			locationY = y + moving.DeltaY;
			if (time % 10 == 0 && Map[x, y + 1] == null && locX == x)
			{

				Map[locationX, locationY + 1] = new FakeSack();
				timer.Start();
			}
			return moving;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			return true;
		}

		public int GetDrawingPriority()
		{
			return 4;
		}
		private void SpawnMob(object sender, EventArgs args)
		{
			time++;
			if (locX == locationX)
				Map[locationX, locationY + 1] = new FakeSack();
		}
		public string GetImageFileName()
		{
			return "Boss.png";
		}
	}
}