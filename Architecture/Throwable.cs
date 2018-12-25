using System.Net.Json;
using System.Windows;
using Digger.Mobs;

namespace Digger.Architecture
{
    public abstract class Throwable:GameObject
    {
        public Throwable()
        {
            _direction = new Vector(-1,0);
        }
        public Throwable(Direction direction)
        {
            _dirIndex =(int) direction;
            _direction = DirectionHelper.GetVec(direction);
        }
		
        public Throwable(int direction)
        {
            _dirIndex = direction;
            _direction = DirectionHelper.GetVec(direction);
        }
        
        
        public Vector _direction;
        public int _dirIndex;
        
        public override CreatureCommand Update(int x, int y)
        {
            int dx = (int) _direction.X;
            int dy = (int) _direction.Y;
            try
            {
                if (Game._map[x + dx, y + dy] == null ||
                    !Game._map[x + dx, y + dy].IsSolidObject() || Game._map[x + dx, y + dy].IsFlammable(this))
                {
                    CreatureCommand cc = new CreatureCommand(dx, dy, this);
                    return cc;
                }
            }
            catch
            {
                // ignored
            }

            return new CreatureCommand(0, 0, this);
        }
        
        public new static PreparedObject FromJsonObject(JsonObjectCollection jsonObject)
        {
            var po = GameObject.FromJsonObject(jsonObject);
            var dir = 3;
            foreach (var obj in jsonObject)
            {
                if (obj is JsonNumericValue n)
                {
                    if (n.Name == "direction")
                    {
                        dir = (int) n.Value;
                    }
                }
            }

            ((Throwable) po._obj)._direction = DirectionHelper.GetVec(dir);
            return po;
        }
    }
}