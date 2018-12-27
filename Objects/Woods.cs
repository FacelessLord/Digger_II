using Digger.Objects.Api;

namespace Digger.Objects
{
	public class Woods : MovableObject
	{
		public override string GetImageFileName()
		{
			return "Woods.png";
		}

		public override int GetDrawingPriority()
		{
			return 1;
		}

		public override bool IsSolidObject()
		{
			return true;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return conflictedGameObject is FireBall || conflictedGameObject.IsSolidObject();
		}

		public override bool IsFlammable(GameObject fireSource)
		{
			return true;
		}
		
		public override bool CanBeTaken()
		{
			return true;
		}
	}
}