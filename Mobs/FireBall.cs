using System.Net.Json;
using System.Windows;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBall : Throwable,IFieryObject
	{

		public int DirIndex
		{
			get => _dirIndex;
			set
			{
				_dirIndex = value;
				_direction = DirectionHelper.GetVec(value);
			}
		}

		public override CreatureCommand Update(int x, int y)
		{
			var command =  base.Update(x, y);

			if (command._deltaX == 0 && command._deltaY == 0)
			{
				var request = new SpawnRequest(null, x, y, true);
				Game.RequestSpawn(request);
			}

			return command;
		}

		public FireBall()
		{
			_direction = new Vector(-1,0);
		}
		public FireBall(Direction direction):base(direction)
		{
			_dirIndex =(int) direction;
			_direction = DirectionHelper.GetVec(direction);
		}
		
		public FireBall(int direction):base(direction)
		{
			_dirIndex = direction;
			_direction = DirectionHelper.GetVec(direction);
		}

		public override string GetImageFileName()
		{
			return "Fireball_"+DirIndex+".png";
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return true;
		}

		public bool DestroyedWhenCollideWith(GameObject obj)
		{
			return true;
		}
	}
}