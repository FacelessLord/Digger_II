using System;
using System.Linq;
using System.Net.Json;
using System.Reflection;
using System.Windows.Forms;

namespace Digger.Architecture
{
	public class Chunk
	{
		private readonly int _x;
		private readonly int _y;

		private readonly int _width = 1;
		private readonly int _height = 1;

		private readonly string _type = "Terrain";
		private readonly JsonObjectCollection _collection;

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

		public GameObject[,] Print(GameObject[,] map)
		{
			for (var i = 0; i < _width; i++)
			for (var j = 0; j < _height; j++)
				map[ _y + j,_x + i] = CreateObject()._obj;
			return map;
		}

		public PreparedObject CreateObject()
		{
			var type = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.FirstOrDefault(z => z.Name == _type);

			if (type == null)
			{
				throw new Exception($"Can't find type '{_type}'");
			}

			var method = type.GetMethods().FirstOrDefault(m => m.Name == "FromJsonObject");
			if (method != null)
			{
				return (PreparedObject) method.Invoke(null, new object[] {_collection});
			}
			else
			{
				return GameObject.FromJsonObject(_collection);
			}
		}
	}
}