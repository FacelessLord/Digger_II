using System.Net.Json;
using Digger.Architecture;
using static Digger.Game;

namespace Digger.Mobs
{
	public class Sack : Throwable
	{
		public int _freeFall; //количетво клеток свободного падения 
		public bool _isFalling;

		public override int GetDrawingPriority()
		{
			return 8;
		}

		public override string GetImageFileName()
		{
			return "Sack.png";
		}

		public override CreatureCommand Update(int x, int y)
		{
			var command = base.Update(x, y);

			if (command._deltaX != 0 || command._deltaY != 0)
			{
				_isFalling = true;
			}
			else
			{
				if (_isFalling)
				{
					Game.RequestSpawn(new SpawnRequest(new Gold(), x,y, true));
					_isFalling = false;
				}
			}

			if (Game._map[x + command._deltaX, y + command._deltaY] is Gold)
			{
				command = new CreatureCommand(0,0);
				DirIndex = -1;
			}
			
			return command;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return false;
		}

		public override bool IsSolidObject()
		{
			return true;
		}
	}
}