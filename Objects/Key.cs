using Digger.Objects.Api;

namespace Digger.Objects
{
    public class Key : MovableObject
    {
        public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
        {
            return conflictedGameObject is Door || conflictedGameObject.IsSolidObject();
        }

        public override int GetDrawingPriority()
        {
            return 11;
        }

        public override string GetImageFileName()
        {
            return "Key.png";
        }

        public override bool IsSolidObject()
        {
            return true;
        }

        public override bool CanBeTaken()
        {
            return true;
        }
    }
}