using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
	class Player : ICreature
	{
		public int XLoc;
		public int YLoc;

		public int GetDrawingPriority()
		{
			return 9;
		}

		public string GetImageFileName()
		{
			return "Digger.png";
		}

		public CreatureCommand Act(int x, int y)
		{

			var moving = new CreatureCommand();
			switch (KeyPressed)
			{
				case Keys.Up:
					if (y >= 0)
						moving.DeltaY--;
					break;
				case Keys.Down:
					if (y <= MapHeight)
						moving.DeltaY++;
					break;
				case Keys.Right:
					if (x <= MapWidth)
						moving.DeltaX++;
					break;
				case Keys.Left:
					if (x >= 0)
						moving.DeltaX--; //движение и последующая проверка
					break;
			}

			if (x + moving.DeltaX >= MapWidth || x + moving.DeltaX < 0)
				moving.DeltaX = 0;
			if (y + moving.DeltaY >= MapHeight || y + moving.DeltaY < 0) //условия
				moving.DeltaY = 0;
			if (Map[x + moving.DeltaX, y + moving.DeltaY] != null && Map[x + moving.DeltaX, y + moving.DeltaY].IsSolidObject())
			{
				moving.DeltaX = 0;
				moving.DeltaY = 0;
			}

			XLoc = x + moving.DeltaX;
			YLoc = y + moving.DeltaY;
			locX = XLoc; //запись в поля Player
			locY = YLoc;
			return moving;
		}

		public bool IsSolidObject()
		{
			return false;
		}

		public bool DeadInConflict(ICreature conflictedObject)
		{
			bool result = conflictedObject is Sack || conflictedObject is Monster || conflictedObject is FakeSack;
			if (result) Game.IsOver = true; //game is over 
			return result;
		}
	}
}