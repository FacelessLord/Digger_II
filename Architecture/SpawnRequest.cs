using System.Windows;

namespace Digger.Architecture
{
	public class SpawnRequest
	{
		/// <summary>
		/// Object to spawn
		/// </summary>
		public GameObject _obj;
		/// <summary>
		/// Replace existing objects?
		/// </summary>
		public bool _forceSpawn = false;

		/// <summary>
		/// X spawn coodrdinate
		/// </summary>
		public int _x;
		/// <summary>
		/// Y spawn Coordinate
		/// </summary>
		public int _y;

		/// <summary>
		/// Time(in ticks) after which request will be satisfied
		/// </summary>
		public int _delay = 0;
		
		public delegate Vector SearchMethod(int x,int y);

		public SearchMethod _searchMethod = (x, y) => new Vector(x, y);

		public SpawnRequest(GameObject obj, int x, int y)
		{
			_obj = obj;
			_x = x;
			_y = y;
		}
		
		public SpawnRequest(GameObject obj, int x, int y,bool forceSpawn)
		{
			_obj = obj;
			_x = x;
			_y = y;
			_forceSpawn = forceSpawn;
		}

		public SpawnRequest SetSearchMethod(SearchMethod method)
		{
			this._searchMethod = method;
			return this;
		}

		public void SetForceSpawn(bool forceSpawn)
		{
			_forceSpawn = forceSpawn;
		}
		public void SetDelay(int delay)
		{
			_delay = delay;
		}

		public override string ToString()
		{
			return (_obj == null ? "null" : _obj.GetImageFileName()) + "|" + _x + "|" + _y + "|" + _forceSpawn;
		}
	}
}