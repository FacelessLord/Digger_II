using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Objects.Api
{
	public abstract class MovableObject : GameObject
	{
		public override CreatureCommand Update(int x, int y)
		{
			var moving = new CreatureCommand(0, 0,this);

			if (!Game._state._moveRequests.ContainsKey(this))
			{
				if ((Game._map[x + 1, y] is Player || Game._map[x - 1, y] is Player ||
				     Game._map[x, y + 1] is Player || Game._map[x, y - 1] is Player) && !Game._shift && Game._control)
				{
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
					if (target != null && !target.CanBeDisplacedBy(this))
					{
						moving._deltaX = 0;
						moving._deltaY = 0;
					}
					else
					{
						Game._player._motionEnabled = true;
					}
				}
			}

			return moving;
		}
	}
}