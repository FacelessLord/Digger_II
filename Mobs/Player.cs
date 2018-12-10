using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
	class Player : ILiving
	{
		public int _xLoc;
		public int _yLoc;
		private int _blocksLeft = 0;

		public int GetDrawingPriority()
		{
			return 9;
		}

		public string GetImageFileName()
		{
			return "Digger.png";
		}

		public CreatureCommand Update(int x, int y)
		{

			var moving = new CreatureCommand();
			switch (_keyPressed)
			{
				case Keys.Up:
					if (y >= 0)
						moving._deltaY--;
					break;
				case Keys.Down:
					if (y <= MapHeight)
						moving._deltaY++;
					break;
				case Keys.Right:
					if (x <= MapWidth)
						moving._deltaX++;
					break;
				case Keys.Left:
					if (x >= 0)
						moving._deltaX--; //движение и последующая проверка
					break;
			}

			if (x + moving._deltaX >= MapWidth || x + moving._deltaX < 0)
				moving._deltaX = 0;
			if (y + moving._deltaY >= MapHeight || y + moving._deltaY < 0) //условия
				moving._deltaY = 0;
			if (_map[x + moving._deltaX, y + moving._deltaY] != null &&
			    _map[x + moving._deltaX, y + moving._deltaY].IsSolidObject())
			{
				moving._deltaX = 0;
				moving._deltaY = 0;
			}

			_xLoc = x + moving._deltaX;
			_yLoc = y + moving._deltaY;
			_locX = _xLoc; //запись в поля Player
			_locY = _yLoc;
			return moving;
		}

		public bool IsSolidObject()
		{
			return false;
		}

		public bool DestroyedInConflict(IObject conflictedObject)
		{
			bool result = conflictedObject is Sack || conflictedObject is Monster || conflictedObject is FakeSack;
			if (result) Game._isOver = true; //game is over 
			return result;
		}


		public bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}
	}
}