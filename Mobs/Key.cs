using System;
using System.Net.Json;
using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
    public class Key : GameObject
    {
        public int _xLoc;
        public int _yLoc;

        public override CreatureCommand Update(int x, int y)
        {
            var moving = new CreatureCommand(0, 0);

            if ((Game._map[x + 1, y] is Player ||Game. _map[x - 1, y] is Player ||
                 Game._map[x, y + 1] is Player || Game._map[x, y - 1] is Player) && !Game._shift )
            switch (Game._keyPressed)
            {
                case Keys.Up:
                    if (y >= 0)
                        moving._deltaY--;
                    break;
                case Keys.Down:
                    if (y <= Game.MapHeight)
                        moving._deltaY++;
                    break;
                case Keys.Right:
                    if (x <= Game.MapWidth)
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
            var target = _map[x + moving._deltaX, y + moving._deltaY];
            if (target != null && target.IsSolidObject() && !( target is Door)
                || target is Living)
            {
                moving._deltaX = 0;
                moving._deltaY = 0;
            }

            _xLoc = x + moving._deltaX;
            _yLoc = y + moving._deltaY;

            return moving;
        }

        public override bool DestroyedInConflict(GameObject conflictedGameObject, params int[] coords)
        {
            return conflictedGameObject is Door || conflictedGameObject.IsSolidObject();
        }

        public override int GetDrawingPriority()
        {
            return 11;
        }

        public override string GetImageFileName()
        {
            return "Key.png";
        }

        public override bool IsSolidObject()
        {
            return true;
        }

        public override bool CanBeTaken()
        {
            return true;
        }
    }
}