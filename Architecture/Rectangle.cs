using System.Net.Json;

namespace Digger.Architecture
{
    public class Rectangle:Chunk
    {
        public Rectangle(JsonObjectCollection collection) : base(collection)
        {
        }

        public override GameObject[,] Print(GameObject[,] map)
        {
            for (var i = 0; i < _width; i++)
            for (var j = 0; j < _height; j++)
                map[ _y + j,_x + i] = JsonMapCreator.CreateObject(_type,_collection)._obj;
            return map;
        }
    }
}