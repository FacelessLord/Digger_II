using System.Windows.Forms;
using Digger.Architecture;
using Digger.Objects.Api;

namespace Digger.Objects
{
	public class Player : Living
	{
		public int _xLoc;
		public int _yLoc;
		private int _blocksLeft = 0;
		public GameObject _inventory;

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override string GetImageFileName()
		{
			return "Specter.png";
		}

		public bool _motionEnabled = false;

		public override CreatureCommand Update(int x, int y)
		{
			if (Game._player == null)
			{
				Game._player = this;
			}

			var moving = new CreatureCommand(0, 0);

			if (!Game._shift && (!Game._control || _motionEnabled))
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
							moving._deltaX--; //движение и последующая проверка
						break;
				}


				if (x + moving._deltaX >= Game.MapWidth || x + moving._deltaX < 0)
					moving._deltaX = 0;
				if (y + moving._deltaY >= Game.MapHeight || y + moving._deltaY < 0) //условия
					moving._deltaY = 0;
				if (Game._map[x + moving._deltaX, y + moving._deltaY] != null &&
				    !Game._map[x + moving._deltaX, y + moving._deltaY].CanBeDisplacedBy(this))
				{
					moving._deltaX = 0;
					moving._deltaY = 0;
				}
			}
			_motionEnabled = false;
			_xLoc = x + moving._deltaX;
			_yLoc = y + moving._deltaY;
			Game._locX = _xLoc; //запись в поля Player
			Game._locY = _yLoc;
			return moving;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			bool result = conflictedGameObject is Sack || conflictedGameObject is Monster ||
			              conflictedGameObject is FakeSack || conflictedGameObject is FireBall ||
			              conflictedGameObject is FireBlock;
			if (result)
			{
				Game._isOver = true;
				//game is over
			}

			return result;
		}


		public override bool CanCreateBlocks(int x, int y)
		{
			return _blocksLeft > 0;
		}

		public void OnKeyPressed(KeyEventArgs e)
		{

			Game._shift = e.Shift;
			Game._control = e.Control;
			if (e.Shift)
			{
				int x = Game._locX;
				int y = Game._locY;
				int dx = 0;
				int dy = 0;
				switch (e.KeyCode)
				{
					case Keys.Up:
						if (y >= 0)
							dy--;
						break;
					case Keys.Down:
						if (y <= Game.MapHeight)
							dy++;
						break;
					case Keys.Right:
						if (x <= Game.MapWidth)
							dx++;
						break;
					case Keys.Left:
						if (x >= 0)
							dx--; //движение и последующая проверка
						break;
				}

				if (dx != 0 || dy != 0)
				{
					if (_inventory == null)
					{
						if (Game._map[x + dx, y + dy] != null && Game._map[x + dx, y + dy].CanBeTaken())
						{
							Game.RequestSpawn(null, x + dx, y + dy, true);
							_inventory = Game._map[x + dx, y + dy];
							//_map[x + dx, y + dy] = null;
						}
					}
					else
					{
						if (_inventory.CanBePlaced(x + dx, y + dy))
						{
							Game.RequestSpawn(_inventory, x + dx, y + dy,
								_inventory.CanBePlaced(x + dx, y + dy));
							//_map[x + dx, y + dy] = _inventory;
							_inventory = null;
						}
					}
				}
				Game._keyPressed = Keys.None;
			}
			if (e.Control)
			{
				int x = Game._locX;
				int y = Game._locY;
				int dx = 0;
				int dy = 0;
				switch (e.KeyCode)
				{
					case Keys.Up:
						if (y >= 0)
							dy--;
						break;
					case Keys.Down:
						if (y <= Game.MapHeight)
							dy++;
						break;
					case Keys.Right:
						if (x <= Game.MapWidth)
							dx++;
						break;
					case Keys.Left:
						if (x >= 0)
							dx--; //движение и последующая проверка
						break;
				}

				if (dx != 0 || dy != 0)
				{
					if (Game._map[x - dx, y - dy] is MovableObject moTo)
					{
						if (Game._map[x + dx, y + dy] == null || Game._map[x + dx, y + dy].CanBeDisplacedBy(this))
						{
							Game.RequestedMotion(this,new CreatureCommand(dx,dy));
							Game.RequestedMotion(moTo,new CreatureCommand(dx,dy));
							Game._keyPressed = Keys.None;
						}
					}
				}
			}
		}
	}
}