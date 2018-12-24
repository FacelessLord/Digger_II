using System.Windows.Forms;

namespace Digger.Architecture
{
	public class RespawnSystem
	{
		public static void CallRespawn()
		{
			Game._keyPressed = Keys.None;
			Game._isOver = false;
			Game._scores = 0;
			var map = JsonMapCreator.CreateMap(Game._mapName);
			Game._window._gameState = new GameState();
			Game._state = Game._window._gameState;
			for (var i = 0; i < Game.MapHeight; i++)
			for (var j = 0; j < Game.MapWidth; j++)
			{
				Game.RequestSpawn(new SpawnRequest(map[i, j], i, j,true).SetDelay(0));
			}
			Game._state._respawning = true;
			//Game._map = map;
			Game._time = 0;
			Game._gameTime = $"{Game._time / 3600:d2}:{(Game._time / 60) % 60:d2}:{Game._time % 60:d2}";
			Game._keyPressed = Keys.None;
			Game._window._pressedKeys.Clear();
		}
	}
}