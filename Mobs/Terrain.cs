namespace Digger.Mobs
{
	class Terrain : ICreature
	{
		public CreatureCommand Act(int x, int y)
		{
			return new CreatureCommand()
			{
				DeltaX = 0,
				DeltaY = 0,
				TransformTo = this
			};
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			return true;
		}

		public int GetDrawingPriority()
		{
			return 8;
		}

		public string GetImageFileName()
		{
			return "Terrain.png";
		}
	}
}