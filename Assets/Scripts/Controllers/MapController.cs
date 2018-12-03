using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Controllers
{
    public class MapController : MonoBehaviour
    {
        [Header("UI Elements")]
        public Transform MapGridParentTransform = null;

        [Header("Prefab References")]
        public GameObject MapTilePrefabReference = null;

        private Dictionary<Coordinate, MapTile> coordinateToMapTile { get; set; } = new Dictionary<Coordinate, MapTile>();

        private Dictionary<string, Coordinate> entityIdToCoordinate { get; set; } = new Dictionary<string, Coordinate>();

        private const int MAP_DIMENSION = 50;

        private void Awake()
        {
            SetupMap("Maps/map1");
        }

        public void AddToMap(Coordinate coordinate, Entity entity)
        {
            coordinateToMapTile[coordinate].entities.Add(entity);

            entityIdToCoordinate[entity.id] = coordinate;
        }

        public Coordinate GetPositionOfEntity(string id)
        {
            if (entityIdToCoordinate.ContainsKey(id))
            {
                return entityIdToCoordinate[id];
            }

            return null;
        }

        public MapTile GetMapTileAtCoordinate(Coordinate coordinate)
        {
            return coordinateToMapTile[coordinate];
        }

        public void RemoveFromMap(Entity entity)
        {
            if (entityIdToCoordinate.ContainsKey(entity.id))
            {
                Coordinate current = entityIdToCoordinate[entity.id];

                if (coordinateToMapTile.ContainsKey(current))
                {
                    coordinateToMapTile[current].entities.Remove(entity);
                }

                entityIdToCoordinate.Remove(entity.id);
            }
        }

        public void MoveEntity(Coordinate coordinate, Entity entity)
        {
            if (GetPositionOfEntity(entity.id) != null)
            {
                RemoveFromMap(entity);
                AddToMap(coordinate, entity);
            }
        }

        private void SetupMap(string mapFilePath)
        {
            UnityEngine.Object map = Resources.Load(mapFilePath);
            TextAsset mapText = map as TextAsset;

            string[] allMapText = mapText.text.Split('\n');

            for (int i = 0; i < MAP_DIMENSION; i++)
            {
                string[] lineMapText = allMapText[i].Split(',');
                for (int j = 0; j < MAP_DIMENSION; j++)
                {
                    Coordinate coordinate = new Coordinate(i, j);

                    coordinateToMapTile[coordinate] = StringToMapTile(lineMapText[j]);
                }
            }
        }

        private MapTile StringToMapTile(string str)
        {
            Models.Terrain terrain = Models.Terrain.NONE;
            Resource resource = Resource.NONE;

            char[] charArr = str.ToCharArray();

            terrain = CharToTerrain(charArr[0]);

            if (charArr.Length > 1)
            {
                resource = StringToResource(str.Substring(1));
            }

            GameObject newMapTileWidgetInstance = Instantiate(MapTilePrefabReference, MapGridParentTransform);

            MapTile mapTile = ScriptableObject.CreateInstance<MapTile>();
            mapTile.terrain = terrain;
            mapTile.resource = resource;

            newMapTileWidgetInstance.GetComponent<MapTileWidget>().mapTile = mapTile;

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