namespace Digger.Architecture
{
	public class SpawnRequest
	{
		/// <summary>
		/// Object to spawn
		/// </summary>
		public IObject _obj;
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

		public SpawnRequest(IObject obj, int x, int y)
		{
			_obj = obj;
			_x = x;
			_y = y;
		}
		
		public SpawnRequest(IObject obj, int x, int y,bool forceSpawn)
		{
			_obj = obj;
			_x = x;
			_y = y;
			_forceSpawn = forceSpawn;
		}

		public void SetForceSpawn(bool forceSpawn)
		{
			_forceSpawn = forceSpawn;
		}
	}
}