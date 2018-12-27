using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Json;
using System.Reflection;
using Digger.Objects;
using Digger.Objects.Api;

namespace Digger.Map
{
	/// <summary>
	/// <para>Class that enables you to create maps from JsonFile.</para>
	/// <para>You capable of specifying Object coordinates, type and other properties,
	/// determined by the object you want to create (ex: Turret have field "direction" and "frequency").</para>
	/// <para>You can also create blocks of similar type and specify their properties like this:
	/// "rect,box" (any case) : { "x":_x, "y":_y, "width":_width, "height":_height, "type":_type, properties of an object}.
	/// There values that starts with "_" are actual values.
	/// "type" - is a ClassName for an object you want to create</para>
	///
	/// <para>Map file is a common Json file.
	/// It should contain at least 2 fields: width and height of the map.
	/// You can also specify, whether the map have wall on the edge by value of "walls" : true of false.</para>
	/// </summary>
	public static class JsonMapCreator
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

			var gameObjects = new List<Object>();

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
					ParseInternals(gameObjects,o,0);
				}
			}

			if (width == -1 || height == -1)
			{
				throw new ArgumentException();
			}

			var map = new GameObject[height, width];

			if (walls)
			{
				map = CreateWalls(map,width,height);
			}

			map = InsertCreatures(map, gameObjects);

			return map;
		}

		public static GameObject[,] InsertCreatures(GameObject[,] map,List<Object> gameObjects)
		{
			foreach (var prepObj in gameObjects)
			{
				if (prepObj is PreparedObject p)
					map[p._x, p._y] = p._obj;
				if (prepObj is Chunk c)
					map = c.Print(map);
			}

			return map;
		}

		public static GameObject[,] CreateWalls(GameObject[,] map, int width,int height)
		{
			for (var i = 0; i < width; i++)
			{
				map[0, i] = new Wall();
				map[height - 1, i] = new Wall();
			}

			for (var i = 0; i < height; i++)
			{
				map[i, 0] = new Wall();
				map[i, width - 1] = new Wall();
			}

			return map;
		}

		public static void ParseInternals(List<Object> gameObjects, JsonObjectCollection o,int intCount)
		{
			var name = "";
			if (o.Name.ToLower() == "rect")
			{
				gameObjects.Add(new Rectangle(o));
				return;
			}

			if (o.Name.ToLower() == "box")
			{
				gameObjects.Add(new Box(o));
				return;
			}

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

			gameObjects.Add(CreateObject(name,o));
		}

		public static PreparedObject CreateObject(string type, JsonObjectCollection collection)
		{
			if (type == "")
			{
				collection.Add(new JsonStringValue("type","Terrain"));
				return GameObject.FromJsonObject(collection);
			}
			var typeClass = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.FirstOrDefault(z => z.Name == type);

			if (typeClass == null)
			{
				throw new Exception($"Can't find type '{type}'");
			}
			var method = typeClass.GetMethods().FirstOrDefault(m => m.Name == "FromJsonObject");
			while (method == null && typeClass.BaseType != null)
			{	
				method = typeClass.BaseType.GetMethods().FirstOrDefault(m => m.Name == "FromJsonObject");
				typeClass = typeClass.BaseType;
			}
			if (method != null)
			{
				return (PreparedObject) method.Invoke(null, new object[] {collection});
			}
			else
			{
				return GameObject.FromJsonObject(collection);
			}
		}
	}
}