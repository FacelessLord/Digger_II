namespace Digger.Architecture
{
	public class PreparedObject
	{
		public GameObject _obj;
		public int _x;
		public int _y;

		public PreparedObject(GameObject obj, int x, int y)
		{
			_obj = obj;
			_x = x;
			_y = y;
		}
	}
}