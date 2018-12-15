using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Digger.Architecture;
using Digger.Mobs;

namespace Digger
{
    public static class CreatureMapCreator
    {
        private static readonly Dictionary<string, Func<IObject>> Factory = new Dictionary<string, Func<IObject>>();

        public static IObject[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong test map '{map}'");
            var result = new IObject[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = CreateCreatureBySymbol(rows[y][x]);
            return result;
        }

        private static IObject CreateObjectByTypeName(string name)
        {
            // Это использование механизма рефлексии. 
            // Ему посвящена одна из последних лекций второй части курса Основы программирования
            // В обычном коде можно было обойтись без нее, но нам нужно было написать такой код,
            // который работал бы, даже если вы ещё не создали класс Monster или Gold. 
            // Просто написать new Gold() мы не могли, потому что это не скомпилировалось бы пока вы не создадите класс Gold.
            if (name.StartsWith("Turret"))
            {
                return new Turret(int.Parse(name.Substring(6)));
            }
            
            if (!Factory.ContainsKey(name))
            {
                var type = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(z => z.Name == name);
                if (type == null)
                    throw new Exception($"Can't find type '{name}'");
                Factory[name] = () => (IObject) Activator.CreateInstance(type);
            }

            return Factory[name]();
        }


        private static IObject CreateCreatureBySymbol(char c)
        {
            switch (c)
            {
                case 'P':
                    return CreateObjectByTypeName("Player");
                case 'T':
                    return CreateObjectByTypeName("Terrain");
                case '0':
                    return CreateObjectByTypeName("Turret0");
                case '1':
                    return CreateObjectByTypeName("Turret1");
                case '2':
                    return CreateObjectByTypeName("Turret2");
                case '3':
                    return CreateObjectByTypeName("Turret3");
                case 'G':
                    return CreateObjectByTypeName("Gold");
                case 'S':
                    return CreateObjectByTypeName("Sack");
                case 'M':
                    return CreateObjectByTypeName("Monster");
                case 'W':
                    return CreateObjectByTypeName("Wall");
                case 'K':
                    return CreateObjectByTypeName("Key");
                case 'D':
                    return CreateObjectByTypeName("Door");
                case 's':
                    return CreateObjectByTypeName("FakeSack");
                case 'F':
                    return CreateObjectByTypeName("FireBlock");
                case 'B':
                    return CreateObjectByTypeName("Boss");
                case 'b':
                    return CreateObjectByTypeName("FireBall");
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }
        }
    }
}