namespace Digger.Architecture
{
    public interface ICreature
    {
	    CreatureCommand Act(int x, int y);
        string GetImageFileName();
        int GetDrawingPriority();
        bool DeadInConflict(ICreature conflictedObject);
	    bool IsSolidObject();
    }
}