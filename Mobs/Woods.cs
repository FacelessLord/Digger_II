using System;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class Woods : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0, 0);

			if ((Game._map[x + 1, y] is Player ||Game. _map[x - 1, y] is Player ||
			    Game._map[x, y + 1] is Player || Game._map[x, y - 1] is Player) && !Game._shift )
				switch (Game._keyPressed)
				{
					case Keys.Up:
						if (y >= 0)
							moving._deltaY--;
						break;
					case Keys.Down:
						if (y <= Game.MapHeight)
							moving._deltaY++;
						break;
					case Keys.Right:
						if (x <= Game.MapWidth)
							moving._deltaX++;
						break;
					case Keys.Left:
						if (x >= 0)
							moving._deltaX--;
						break;
				}
			if (x + moving._deltaX >= Game.MapWidth || x + moving._deltaX < 0)
				moving._deltaX = 0;
			if (y + moving._deltaY >= Game.MapHeight || y + moving._deltaY < 0)
				moving._deltaY = 0;
			var target = Game._map[x + moving._deltaX, y + moving._deltaY];
			if (target != null && target.IsSolidObject() || target is Living)
			{
				moving._deltaX = 0;
				moving._deltaY = 0;
			}

			return moving;
		}

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
			return conflictedGameObject is FireBall;
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