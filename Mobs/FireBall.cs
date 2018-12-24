using System.Net.Json;
using System.Windows;
using Digger.Architecture;

namespace Digger.Mobs
{
	public class FireBall : GameObject,IFieryObject
	{
		public Vector _direction;
		private int _dirIndex;

		public int DirIndex
		{
			get => _dirIndex;
			set
			{
				_dirIndex = value;
				_direction = DirectionHelper.GetVec(value);
			}
		}

		public FireBall()
		{
			_direction = new Vector(-1,0);
		}
		public FireBall(Direction direction)
		{
			_dirIndex =(int) direction;
			_direction = DirectionHelper.GetVec(direction);
		}
		
		public FireBall(int direction)
		{
			_dirIndex = direction;
			_direction = DirectionHelper.GetVec(direction);
		}
		
		public override CreatureCommand Update(int x, int y)
		{
			int dx = (int) _direction.X;
			int dy = (int) _direction.Y;
			try
			{
				if (Game._map[x + dx, y + dy] == null ||
				    !Game._map[x + dx, y + dy].IsSolidObject() || Game._map[x + dx, y + dy].IsFlammable(this))
				{
					CreatureCommand cc = new CreatureCommand(dx, dy, this);
					return cc;
				}
			}
			catch
			{
				// ignored
			}

			var request = new SpawnRequest(null, x, y, true);
			Game.RequestSpawn(request);

			return new CreatureCommand(0, 0, new FireBlock());
		}

		public override string GetImageFileName()
		{
			return "Fireball_"+DirIndex+".png";
		}

		public override int GetDrawingPriority()
		{
			return 0;
		}

		public override bool IsSolidObject()
		{
			return false;
		}

		public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
		{
			return true;
		}

		public bool DestroyedWhenCollideWith(GameObject obj)
		{
			return true;
		}
	}
}