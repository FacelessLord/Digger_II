using System.Windows.Forms;
using Digger.Architecture;

namespace Digger
{
	public static class Game
	{
		private const string NullMap = @"
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW
W   SSSSSTK   K WbbbbbbbbbbbbbbbbbbbbbbW
WP  TTTTTT    K WFGGGGMMGGGGGGMGGGGMGGFW
W     F       b WFGGMGGGGGGGMMGGGGGGGGFW
W         F     WFGGGGGGGGGGGGGGGGGGGGFW
WWWWWWWWWWWWW   DFGGGGMMGGGGGGGGMGGGGGFW
W1         2W   WFGMGGGGGGGGMGGGGGGGGGFW
W  B        W   WFGGGGGGGGGGGGGGGGGMGGFW
W  B  T     D   WFGGMGGGGGGMGGGGGGGMGGFW
W  T  B     D   WFGGGGGGGGGGGGMMGGGGGGFW
W0         3D   WbbbbbbbbbbbbbbbbbbbbbbW
WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW";

		private const string MapWithPlayerTerrainSackGold = @"
PTTGTT TS
TST  TSTT
TTTTTTSTT
T TSTS TT
T TTTG ST
TSTSTT TT";

		private const string MapWithPlayerTerrainSackGoldMonster = @"
PTTGTT TST
TST  TSTTM
TTT TTSTTT
T TSTS TTT
T TTTGMSTS
T TMT M TS
TSTSTTMTTT
S TTST  TG
 TGST MT M
 T  TMTTMT";

		private const string FirstMap = @"
WWWWWWWWWWWWWWWWW
WTKGSSSSSTK W B W
WP TTTTTTTS WW WW
WT W  M  W TW   W
WT WW  MWW  W   W
WWWWWWDWWWWWW   W
WSTSW M W   W   W
WTTTW   WK  WF  W
W   W   D   W   W
W   D   W       W
W   W   W   W   W
WWWWWWWWWWWWWWWWW";

		public static GameObject[,] _map;
		public static int _scores;
		public static string _gameTime = "00:00"; // отрисовка time
		public static int _time; // глобальное время в мс
		public static bool _isOver;
		public static int _locX; //положение Player Х
		public static int _locY; //положение Player Y

		public static Keys _keyPressed; //?
		public static int MapWidth => _map.GetLength(0);
		public static int MapHeight => _map.GetLength(1);
		public static GameState _state;

		public static void CreateMap()
		{
//			_map = CreatureMapCreator.CreateMap(NullMap);
			_map = JsonMapCreator.CreateMap("Maps/map.json");
		}

		public static void RequestSpawn(SpawnRequest request)
		{
			_state._spawnRequests.Add(request);
		}
	}
}