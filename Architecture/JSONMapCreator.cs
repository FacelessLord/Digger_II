using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Json;
using System.Reflection;
using Digger.Mobs;

namespace Digger.Architecture
{
	public class JsonMapCreator
	{
		public static GameObject[,] CreateMap(string mapFile)
		{
			var parser = new JsonTextParser();
			var mapCode = File.ReadAllText(mapFile);
			var obj = parser.Parse(mapCode);
			var mainCollection = (JsonCollection) obj;
			var width = -1;
			var height = -1;
			var walls = true;

			var gameObjects = new List<PreparedObject>();

			foreach (var jsonObj in mainCollection)
			{
				if (jsonObj is JsonNumericValue n)
				{
					if (n.Name == "width")
					{
						width = (int) n.Value;
					}

					if (n.Name == "height")
					{
						height = (int) n.Value;
					}
				}

				if (jsonObj is JsonLiteralValue b)
				{
					if (b.Name == "walls")
					{
						switch (b.Value)
						{
							case JsonAllowedLiteralValues.False:
								walls = false;
								break;
							case JsonAllowedLiteralValues.True:
								walls = true;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
				}

				if (jsonObj is JsonObjectCollection o)
				{
					var name = "";
					foreach (var field in o)
					{
						if (field is JsonStringValue s)
						{
							if (s.Name == "type")
							{
								name = s.Value;
							}
						}
					}

					if (name != "")
					{
						var type = Assembly
							.GetExecutingAssembly()
							.GetTypes()
							.FirstOrDefault(z => z.Name == name);

						if (type == null)
						{
							throw new Exception($"Can't find type '{name}'");
						}

						var method = type.GetMethods().FirstOrDefault(m => { return m.Name == "FromJsonObject"; });
						if (method != null)
						{
							gameObjects.Add(method.Invoke(null, new object[] {o}) as PreparedObject);
						}
						else
						{
							gameObjects.Add(GameObject.FromJsonObject(o));
						}
					}
					else
					{
						o.Add(new JsonStringValue("type","Terrain"));
						gameObjects.Add(GameObject.FromJsonObject(o));
					}
				}
			}

			if (width == -1 || height == -1)
			{
				throw new ArgumentException();
			}

			var map = new GameObject[height,width];

			if (walls)
			{
				for (var i = 0; i < width; i++)
				{
					map[0,i] = new Wall();
					map[height-1,i] = new Wall();
				}
				for (var i = 0; i < height; i++)
				{
					map[i,0] = new Wall();
					map[i,width-1] = new Wall();
				}
			}
			foreach (var prepObj in gameObjects)
			{
				map[prepObj._x, prepObj._y] = prepObj._obj;
			}

			return map;
			/*foreach (var t in mainCollection)
			{
				Console.WriteLine(t+"|"+t.GetType());
			}*/
		}
	}
}