using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Digger.Game;


namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand()
            {
                DeltaX = 0,
                DeltaY = 0,
                TransformTo = this
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 8;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        public int XLoc;
        public int YLoc;
       public int GetDrawingPriority()
        {
            return 9;
        }
        public string GetImageFileName()
        {
            return "Digger.png";
        }
        public CreatureCommand Act(int x, int y)
        {

            var moving = new CreatureCommand();
            switch (KeyPressed)
            {
                case Keys.Up:
                    if (y >= 0 )
                        moving.DeltaY--;
                    break;
                case Keys.Down:
                    if (y <= MapHeight )
                        moving.DeltaY++;
                    break;
                case Keys.Right:
                    if (x <= MapWidth  )
                        moving.DeltaX++;
                    break;
                case Keys.Left:
                    if (x >= 0 )
                        moving.DeltaX--;            //движение и последующая проверка
                    break;
            }
            if (x + moving.DeltaX >= MapWidth || x + moving.DeltaX < 0 )
                moving.DeltaX = 0;
            if (y + moving.DeltaY >= MapHeight || y + moving.DeltaY < 0 ) //условия
                moving.DeltaY = 0;
            if (Map[x + moving.DeltaX, y + moving.DeltaY] is Sack
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Key 
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Wall
                || Map[x + moving.DeltaX, y + moving.DeltaY] is FakeSack
                || Map[x + moving.DeltaX, y + moving.DeltaY] is Door)
            {
                moving.DeltaX = 0;
                moving.DeltaY = 0;
            }
            XLoc = x + moving.DeltaX;
            YLoc = y + moving.DeltaY;
            locX = XLoc; //запись в поля Player
            locY = YLoc;
            return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            bool result = conflictedObject is Sack || conflictedObject is Monster || conflictedObject is FakeSack;
            if (result) Game.IsOver = true; //game is over 
            return result;
        }
    }
    public class Sack : ICreature
    {
        public int FreeFall; //количетво клеток, свободного падения 
        public bool IsFalling;
        public int GetDrawingPriority()
        {
            return 8;
        }
        public string GetImageFileName()
        {
            return "Sack.png";
        }
        public CreatureCommand Act(int x, int y)
        {
            
            var moving = new CreatureCommand{ };
            if (y + 1 < MapHeight && (Map[x, y + 1] == null 
                            || ( FreeFall > 0 && (Map[x,y+1] is Player 
                            || Map[x, y + 1] is Monster) || IsFalling)))
            {
                moving.DeltaY++;
                FreeFall++;
                IsFalling = true;
            }
            if (y + 1 == MapHeight && Map[x, 0] == null && IsFalling)
                moving.DeltaY--;
            if (FreeFall > 1 && !IsFalling)
                moving.TransformTo = new Gold();
            if (!IsFalling)
                FreeFall = 0;
            if (y + 1 < MapHeight && (Map[x, y + 1] is Terrain || Map[x, y + 1] is Sack))
                IsFalling = false;
            else IsFalling = false;

                return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
                if (time < 3600) //...
                    Scores += 20;
                else
                    Scores += 10;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 6;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }
    public class Monster : ICreature
    {
        string way = "left"; // вектор движения моба, нужно создать специальный класс вектора для "умного" движения мобов
        public CreatureCommand Act(int x, int y)
        {
            var moving = new CreatureCommand { DeltaY = 0 };
            if (time % 6 == 0)
            {
                if ((Map[x + 1, y] == null || Map[x + 1, y] is Player) && way == "left")
                {
                    moving.DeltaX++;
                    way = "left";
                    
                }
                else way = "right"; // - заменить
                
                if ((Map[x - 1, y] == null || Map[x - 1, y] is Player) && way == "right")
                {
                    moving.DeltaX--;
                    way = "right";
                }
                else way = "left"; // - заменить

                if (Map[x + moving.DeltaX, y + moving.DeltaY] is Monster ||
                          (Map[x + moving.DeltaX, y] != null
                            && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Gold)
                            && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Monster)
                            && !(Map[x + moving.DeltaX, y + moving.DeltaY] is Player)))
                {
                    moving.DeltaX = 0;
                }
                
            }

            return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        { 
            return conflictedObject is Sack || conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 11;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }

    }
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
    }
    public class Wall : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Door;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Wall.png";
        }
    }
    public class Door : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Key)
            {
                Scores += 50;
            }
            return conflictedObject is Key;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            //if (false) return "OpenDoor.png";
            return "DoorClose.png";
        }
    }
    public class Boss : ICreature
    {
        int locationX;
        int locationY;
        int time = 0; 
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() ;


        public CreatureCommand Act(int x, int y)
        {

            timer.Interval = 1;
            timer.Tick += SpawnMob;
            
            var moving = new CreatureCommand { };
            locationX = x + moving.DeltaX;
            locationY = y + moving.DeltaY;
            if (time % 10 == 0 && Map[x, y + 1] == null && locX == x)
            {

                Map[locationX, locationY + 1] = new FakeSack();
                timer.Start();
            }
            return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }
        private void SpawnMob(object sender, EventArgs args)
        {
            time++;
            if (locX == locationX)
                Map[locationX, locationY + 1] = new FakeSack();
        }
        public string GetImageFileName()
        {
            return "Boss.png";
        }
    }
    public class FakeSack : ICreature
    {
        int FreeFall;
        bool IsFalling;
        public CreatureCommand Act(int x, int y)
        {
            var moving = new CreatureCommand { DeltaY = 0 };
            if ( y + 1 < MapHeight && (Map[x, y + 1] == null
                            || (FreeFall > 0 && (Map[x, y + 1] is Player
                            || Map[x, y + 1] is Wall
                            || Map[x, y + 1] is Monster) || IsFalling)))
            {
                moving.DeltaY++;
                FreeFall++;
                IsFalling = true;
            }
            if (FreeFall > 1 && !IsFalling)
                moving.TransformTo = new Gold();
            if (!IsFalling)
                FreeFall = 0;
            if (y + 1 < MapHeight && (Map[x, y + 1] is Terrain 
                || Map[x, y + 1] is Sack))
                IsFalling = false;
            else IsFalling = false;
            return moving;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Wall;
        }

        public int GetDrawingPriority()
        {
            return 6;
        }

        public string GetImageFileName()
        {
            return "FakeSack.png";
        }
    }
}