using System.Collections.Generic;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.Utility
{
    public class MapParser
    {
        public Dictionary<Coordinate, MapTile> SetupMap(string mapFilePath, int dimension)
        {
            Dictionary<Coordinate, MapTile> coordinateToMapTile = new Dictionary<Coordinate, MapTile>();
            UnityEngine.Object map = Resources.Load(mapFilePath);
            TextAsset mapText = map as TextAsset;

            string[] allMapText = mapText.text.Split('\n');

            for (int i = 0; i < dimension; i++)
            {
                string[] lineMapText = allMapText[i].Split(',');
                for (int j = 0; j < dimension; j++)
                {
                    Coordinate coordinate = new Coordinate(i, j);

                    coordinateToMapTile[coordinate] = StringToMapTile(lineMapText[j], coordinate);
                }
            }

            return coordinateToMapTile;
        }

        private MapTile StringToMapTile(string str, Coordinate coordinate)
        {
            Models.Terrain terrain = Models.Terrain.NONE;
            Resource resource = Resource.NONE;

            char[] charArr = str.ToCharArray();

            terrain = CharToTerrain(charArr[0]);

            if (charArr.Length > 1)
            {
                resource = StringToResource(str.Substring(1));
            }

            MapTile mapTile = ScriptableObject.CreateInstance<MapTile>();
            mapTile.terrain = terrain;
            mapTile.resource = resource;

            return mapTile;
        }

        private Resource StringToResource(string str)
        {
            Resource resource = Resource.NONE;

            switch (str)
            {
                case "F":
                    resource = Resource.FOOD;
                    break;

                case "Ma":
                    resource = Resource.MANA;
                    break;

                case "Me":
                    resource = Resource.METAL;
                    break;
            }

            return resource;
        }

        private Models.Terrain CharToTerrain(char character)
        {
            Models.Terrain terrain = Models.Terrain.NONE;

            switch (character)
            {
                case 'G':
                    terrain = Models.Terrain.GRASS;
                    break;

                case 'H':
                    terrain = Models.Terrain.HILL;
                    break;

                case 'F':
                    terrain = Models.Terrain.FOREST;
                    break;

                case 'M':
                    terrain = Models.Terrain.MOUNTAIN;
                    break;

                case 'W':
                    terrain = Models.Terrain.WATER;
                    break;

                case 'S':
                    terrain = Models.Terrain.SWAMP;
                    break;

                case 'D':
                    terrain = Models.Terrain.DESERT;
                    break;
            }

            return terrain;
        }

    }
}