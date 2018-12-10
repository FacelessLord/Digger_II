using static Digger.Game;
using System;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class Boss : ICreature
	{
		int locationX;
		int locationY;
		int time = 0;
        bool IsAlive = true;
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() ;


		public CreatureCommand Act(int x, int y)
		{

			timer.Interval = 4;
			timer.Tick +=SpawnMob;
            
			var moving = new CreatureCommand { };
			locationX = x + moving.DeltaX;
			locationY = y + moving.DeltaY;
			if (time % 10 == 0 && Map[x, y + 1] == null && locX == x && locationY < locY && IsAlive)
			{

				Map[locationX, locationY + 1] = new FakeSack();
				timer.Start();
			}
			return moving;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
            IsAlive = conflictedObject is Key || conflictedObject is Player || 
                     conflictedObject is Sack || conflictedObject is FakeSack;

            return IsAlive;
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
			time++;
			if (locX == locationX && locationY > locY && IsAlive)
				Map[locationX, locationY + 1] = new FakeSack();
		}
		public string GetImageFileName()
		{
			return "Boss.png";
		}
	}
}