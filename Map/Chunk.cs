using System.Net.Json;
using Digger.Objects.Api;

namespace Digger.Map
{
	public abstract class Chunk
	{
		public readonly int _x;
		public readonly int _y;

		public readonly int _width = 1;
		public readonly int _height = 1;

		public readonly string _type = "Terrain";
		public readonly JsonObjectCollection _collection;

		public Chunk(JsonObjectCollection collection)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				var entry = collection[i];
				if (entry is JsonNumericValue n)
				{
					if (n.Name == "x")
					{
						_x = (int) n.Value;
					}

					if (n.Name == "y")
					{
						_y = (int) n.Value;
					}

					if (n.Name == "width")
					{
						_width = (int) n.Value;
						collection.RemoveAt(i);
						i--;
					}

					if (n.Name == "height")
					{
						_height = (int) n.Value;
						collection.RemoveAt(i);
						i--;
					}
				}

				if (_width < 0)
				{
					_x += _width;
					_width *= -1;
				}

				if (_height < 0)
				{
					_y += _height;
					_height *= -1;
				}

				if (entry is JsonStringValue s)
				{
					if (s.Name == "type")
					{
						_type = s.Value;
					}
				}
			}

			_collection = collection;
		}

		public abstract GameObject[,] Print(GameObject[,] map);
	}
}