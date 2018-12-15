using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
    public class Key : IObject
    {
        public int _xLoc;
        public int _yLoc;

        public CreatureCommand Update(int x, int y)
        {
            var moving = new CreatureCommand(0,0);

            if (_map[x + 1, y] is Player || _map[x - 1, y] is Player ||
                _map[x, y + 1] is Player || _map[x, y - 1] is Player)
                switch (_keyPressed)
                {
                    case Keys.Up:
                        if (y >= 0)
                            moving._deltaY--;
                        break;
                    case Keys.Down:
                        if (y <= MapHeight)
                            moving._deltaY++;
                        break;
                    case Keys.Right:
                        if (x <= MapWidth)
                            moving._deltaX++;
                        break;
                    case Keys.Left:
                        if (x >= 0)
                            moving._deltaX--;
                        break;
                }
            if (x + moving._deltaX >= MapWidth || x + moving._deltaX < 0)
                moving._deltaX = 0;
            if (y + moving._deltaY >= MapHeight || y + moving._deltaY < 0)
                moving._deltaY = 0;
            if (_map[x + moving._deltaX, y + moving._deltaY] is Sack
                || _map[x + moving._deltaX, y + moving._deltaY] is Key
                || _map[x + moving._deltaX, y + moving._deltaY] is Wall
                || _map[x + moving._deltaX, y + moving._deltaY] is Monster
                || _map[x + moving._deltaX, y + moving._deltaY] is Player)
            {
                moving._deltaX = 0;
                moving._deltaY = 0;
            }

            _xLoc = x + moving._deltaX;
            _yLoc = y + moving._deltaY;

            return moving;
        }

        public bool DestroyedInConflict(IObject conflictedObject)
        {
            return conflictedObject is Door;
        }

        public int GetDrawingPriority()
        {
            return 11;
        }

        public string GetImageFileName()
        {
            return "Key.png";
        }

        public bool IsSolidObject()
        {
            return true;
        }
    }
}