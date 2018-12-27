using System.Windows.Forms;
using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class MetalBox : MovableObject
	{
		public override string GetImageFileName()
		{
			return "MetalBox.png";
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
			return conflictedGameObject.IsSolidObject();
		}

		public override bool IsFlammable(GameObject fireSource)
		{
			return false;
		}
		
		public override bool CanBeTaken()
		{
			return false;
		}
	}
}