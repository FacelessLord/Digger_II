namespace Digger.Architecture
{
	public interface IObject
	{
		CreatureCommand Update(int x, int y);
		string GetImageFileName();
		int GetDrawingPriority();
		bool IsSolidObject();
		bool DestroyedInConflict(IObject conflictedObject);
	}
}