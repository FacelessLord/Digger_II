using System.Windows.Forms;

namespace Digger
{
    public static class Game
    {
        private const string NullMap = @"
WWWWWWWWWWWWWWWWW
W   SSSSSTK   K W
WP  TTTTTT    K W
W                
W               W
WWWWWWWWWWWWW   W
W  BB B     W   W
W  B        W   W
W  B  T       D W
W  T  B       D W
W             D W
WWWWWWWWWWWWWWWWW";

        private const string mapWithPlayerTerrainSackGold = @"
PTTGTT TS
TST  TSTT
TTTTTTSTT
T TSTS TT
T TTTG ST
TSTSTT TT";

        private const string mapWithPlayerTerrainSackGoldMonster = @"
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

        public static ICreature[,] Map;
        public static int Scores;
        public static string GameTime = "00:00"; // отрисовка time
        public static int time; // глобальное время в мс
        public static bool IsOver;
        public static int locX; //положение Player Х
        public static int locY; //положение Player Y

        public static Keys KeyPressed; //?
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap()
        {
            Map = CreatureMapCreator.CreateMap(NullMap);
        }
    }
}