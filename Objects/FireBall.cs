using System.Windows;
using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class FireBall : Throwable,IFieryObject
	{

		public FireBall()
		{
			_direction = new Vector(-1,0);
		}
		public FireBall(Direction direction):base(direction)
		{
			_dirIndex =(int) direction;
			_direction = DirectionHelper.GetVec(direction);
		}

		public override void OnStopped(int x, int y)
		{
			Game.RequestSpawn(new Air(),x,y);
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

		public override bool CanBeDisplacedBy(GameObject obj)
		{
			return true;
		}
		
		public override bool CanBeReplacedBy(GameObject obj)
		{
			return obj is Living || obj.IsSolidObject() || obj is Air;
		}
	}
}