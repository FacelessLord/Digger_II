using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Digger.Architecture;

namespace Digger
{
    public class GameState
    {
        public const int ElementSize = 32;
        public List<CreatureAnimation> _animations = new List<CreatureAnimation>();

        public List<SpawnRequest> _spawnRequests = new List<SpawnRequest>();
        
        public void BeginAct()
        {
            _animations.Clear();
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var creature = Game._map[x, y];
                if (creature == null) continue;
                var command = creature.Update(x, y);

                if (x + command._deltaX < 0 || x + command._deltaX >= Game.MapWidth || y + command._deltaY < 0 ||
                    y + command._deltaY >= Game.MapHeight)
                    throw new Exception($"The object {creature.GetType()} falls out of the game field");

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
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                Game._map[x, y] = SelectWinnerCandidatePerLocation(creaturesPerLocation, x, y);
            var removal = new List<SpawnRequest>();
            foreach (var request in _spawnRequests)
            {
                if (request._delay <= 0)
                {
                    if (Game._map[request._x, request._y] == null || request._forceSpawn)
                    {
                        Game._map[request._x, request._y] = request._obj;
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
                if (rival != candidate && candidate.DestroyedInConflict(rival))
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