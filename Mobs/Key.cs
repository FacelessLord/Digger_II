using static Digger.Game;
using System.Windows.Forms;
using Digger.Architecture;

namespace Digger.Mobs
{
    public class Key : ICreature
    {
        public int XLoc;
        public int YLoc;

        public CreatureCommand Act(int x, int y)
        {


            var moving = new CreatureCommand();


            if (Map[x + 1, y] is Player || Map[x - 1, y] is Player ||
                Map[x, y + 1] is Player || Map[x, y - 1] is Player)
                switch (KeyPressed)
                {
                    case Keys.Up:
                        if (y >= 0)
                            moving.DeltaY--;
                        break;
                    case Keys.Down:
                        if (y <= MapHeight)
                            moving.DeltaY++;
                        break;
                    case Keys.Right:
                        if (x <= MapWidth)
                            moving.DeltaX++;
                        break;
                    case Keys.Left:
                        if (x >= 0)
                            moving.DeltaX--;
                        break;
                }
            if (x + moving.DeltaX >= MapWidth || x + moving.DeltaX < 0)
                moving.DeltaX = 0;
            if (y + moving.DeltaY >= MapHeight || y + moving.DeltaY < 0)
                moving.DeltaY = 0;
            if (Map[x + moving.DeltaX, y + moving.DeltaY] is Sack
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Key
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Wall
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Monster
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Player)
            {
                moving.DeltaX = 0;
                moving.DeltaY = 0;
            }

            XLoc = x + moving.DeltaX;
            YLoc = y + moving.DeltaY;

            return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
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