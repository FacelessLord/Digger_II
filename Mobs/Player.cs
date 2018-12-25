using System;
using System.Collections.Generic;
using System.Net.Json;
using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
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

		public override CreatureCommand Update(int x, int y)
		{
			if (Game._player == null)
			{
				Game._player = this;
			}

			var moving = new CreatureCommand(0, 0);

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
				_isOver = true;
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
			Game._alt = e.Alt;
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
							var req = new SpawnRequest(null, x + dx, y + dy, true);
							Game.RequestSpawn(req);
							_inventory = Game._map[x + dx, y + dy];
							//_map[x + dx, y + dy] = null;
						}
					}
					else
					{
						if (_inventory.CanBePlaced(x + dx, y + dy))
						{
							var req = new SpawnRequest(_inventory, x + dx, y + dy,
								_inventory.CanBePlaced(x + dx, y + dy));
							Game.RequestSpawn(req);
							//_map[x + dx, y + dy] = _inventory;
							_inventory = null;
						}
					}
				}

				Game._keyPressed = Keys.None;
			}
		}
	}
}