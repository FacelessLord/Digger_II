using System;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class MonsterPortal : GameObject
	{
		private int _timer = 1;
		private int _speed = 8;
		
		public override CreatureCommand Update(int x, int y)
		{
			_timer = (_timer + 1) % _speed;
			if (_timer == 0)
			{
				int dir = new Random().Next(4);
				int dir2 = new Random().Next(4);
				var vec = DirectionHelper.GetVec(dir);
				var vec2 = DirectionHelper.GetVec(dir2);
				var request = new SpawnRequest(new Monster(),x+(int)vec.X+(int)vec2.X,y+(int)vec.Y+(int)vec2.Y);
				Game.RequestSpawn(request);
			}
			
			return new CreatureCommand(0,0,this);
		}

		public override string GetImageFileName()
		{
			return "Portal.png";
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override bool IsSolidObject()
		{
			return true;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return false;
		}
	}
}