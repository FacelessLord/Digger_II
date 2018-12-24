using System.Windows.Forms;
using Digger.Architecture;
using Digger.Mobs;

namespace Digger
{
	public static class Game
	{
		public static GameObject[,] _map;
		public static int _scores;
		public static string _gameTime = "00:00"; // Format time string used in drawing
		public static int _time; // Global time (ms)
		public static bool _isOver;
		public static int _locX; //Player Х
		public static int _locY; //Player Y
		public static Player _player;
		public static bool _shift = false;
		public static bool _alt = false;

		public static Keys _keyPressed; //Holds the value of last pressed key
		public static int MapWidth => _map.GetLength(0);
		public static int MapHeight => _map.GetLength(1);
		public static GameState _state;
		public static DiggerWindow _window;

		public static string _mapName = "Maps/map.json";

		public static void CreateMap()
		{
			_map = JsonMapCreator.CreateMap(_mapName);
		}

		public static void RequestSpawn(SpawnRequest request)
		{
			_state._spawnRequests.Add(request);
		}
		
		public static void SpawnRequestedObjects()
		{
			_state.SpawnRequestedObjects();
		}
	}
}