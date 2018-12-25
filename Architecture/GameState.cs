using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger
{
    public class GameState
    {
        public const int ElementSize = 32;
        public List<CreatureAnimation> _animations = new List<CreatureAnimation>();

        public List<SpawnRequest> _spawnRequests = new List<SpawnRequest>();

        public bool _respawning = false;
        
        public void BeginAct()
        {
            _animations.Clear();
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var creature = Game._map[x, y];
                if (creature == null) continue;
                var command = creature.Update(x, y);

                if (y + command._deltaY < 0 || y + command._deltaY >= Game.MapHeight)
                {
                    command._deltaY = 0;
                }

                if (x + command._deltaX < 0 || x + command._deltaX >= Game.MapWidth)
                {
                    command._deltaX = 0;
                }
                   // throw new Exception($"The object {creature.GetType()} falls out of the game field");

                _animations.Add(
                    new CreatureAnimation
                    {
                        _command = command,
                        _creature = creature,
                        _location = new Point(x * ElementSize, y * ElementSize),
                        _targetLogicalLocation = new Point(x + command._deltaX, y + command._deltaY)
                    });
            }

            _animations = _animations.OrderByDescending(z => z._creature.GetDrawingPriority()).ToList();
        }

        public void EndAct()
        {
            var creaturesPerLocation = GetCandidatesPerLocation();
            if (!_respawning)
            {
                for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    Game._map[x, y] = SelectWinnerCandidatePerLocation(creaturesPerLocation, x, y);
            }
            else
            {
                _respawning = false;
            }

            if (Game._isOver)
            {
//                var respResult = MessageBox.Show(Game._window, "Do you wish to respawn?",
//                    "Respawn", MessageBoxButtons.OK);
//                if (respResult == DialogResult.OK)
                {
                    RespawnSystem.CallRespawn();
                }
            }

            SpawnRequestedObjects();
        }

        public void SpawnRequestedObjects()
        {
            
            var removal = new List<SpawnRequest>();
            foreach (var request in _spawnRequests)
            {
                if (request._delay <= 0)
                {
                    int tx = request._x;
                    int ty = request._y;
                    var vec = request._searchMethod(tx,ty);
                    tx =(int) vec.X;
                    ty = (int) vec.Y;
                    if (Game._map[tx, ty] == null || request._forceSpawn)
                    {
                        Game._map[tx, ty] = request._obj;
                    }
                    removal.Add(request);
                }
                else
                {
                    request._delay-=1;
                }
            }

            foreach (var request in removal)
            {
                _spawnRequests.Remove(request);
            }
        }

        private static GameObject SelectWinnerCandidatePerLocation(List<GameObject>[,] creatures, int x, int y)
        {
            var candidates = creatures[x, y];
            var aliveCandidates = candidates.ToList();
            foreach (var candidate in candidates)
            foreach (var rival in candidates)
                if (rival != candidate && candidate.DestroyedInConflict(rival,x,y))
                    aliveCandidates.Remove(candidate);
            if (aliveCandidates.Count > 1)
                throw new Exception(
                    $"Creatures {aliveCandidates[0].GetType().Name} and {aliveCandidates[1].GetType().Name} claimed the same map cell");

            return aliveCandidates.FirstOrDefault();
        }

        private List<GameObject>[,] GetCandidatesPerLocation()
        {
            var creatures = new List<GameObject>[Game.MapWidth, Game.MapHeight];
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                creatures[x, y] = new List<GameObject>();
            foreach (var e in _animations)
            {
                var x = e._targetLogicalLocation.X;
                var y = e._targetLogicalLocation.Y;
                var nextCreature = e._command._transformTo ?? e._creature;
                creatures[x, y].Add(nextCreature);
            }

            return creatures;
        }
    }
}