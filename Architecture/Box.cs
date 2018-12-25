using System.Net.Json;

namespace Digger.Architecture
{
    public class Box : Chunk
    {
        public Box(JsonObjectCollection collection) : base(collection)
        {
        }

        public override GameObject[,] Print(GameObject[,] map)
        {
            for (var i = 0; i < _width; i++)
            {
                map[_x + i, _y + 0] = JsonMapCreator.CreateObject(_type, _collection)._obj;
                map[_x + i, _y + _height - 1] = JsonMapCreator.CreateObject(_type, _collection)._obj;
            }

            for (var j = 0; j < _height; j++)
            {
                map[_x + 0, _y + j] = JsonMapCreator.CreateObject(_type, _collection)._obj;
                map[_x + _width - 1, _y + j] = JsonMapCreator.CreateObject(_type, _collection)._obj;
            }

            return map;
        }
    }
}