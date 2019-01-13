using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.Controllers
{
    public class MapTileAddedArgs
    {
        public MapTile mapTile;
        public Coordinate coordinate;
    }

    public class EntityAddedArgs
    {
        public Entity entity;
        public Coordinate coordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/Map Controller")]
    public class MapController : ScriptableObject
    {
        public event EventHandler<MapTileAddedArgs> MapTileAdded;
        public event EventHandler<EntityAddedArgs> EntityAdded;
        private Dictionary<Coordinate, MapTile> m_CoordinateToMapTile = new Dictionary<Coordinate, MapTile>();
        private Dictionary<string, Coordinate> m_EntityIdToCoordinate = new Dictionary<string, Coordinate>();

        private const int MAP_DIMENSION = 50;

        public void AddToMap(Coordinate coordinate, Entity entity)
        {
            MapTile mapTile = m_CoordinateToMapTile[coordinate];

            mapTile.entities.Add(entity);

            m_EntityIdToCoordinate[entity.id] = coordinate;

            EntityAdded?.Invoke(this, new EntityAddedArgs()
            {
                entity = entity,
                coordinate = coordinate
            });
        }

        public Coordinate GetPositionOfEntity(string id)
        {
            if (m_EntityIdToCoordinate.ContainsKey(id))
            {
                return m_EntityIdToCoordinate[id];
            }

            return null;
        }

        public MapTile GetMapTileAtCoordinate(Coordinate coordinate)
        {
            return m_CoordinateToMapTile[coordinate];
        }

        public void RemoveFromMap(Entity entity)
        {
            if (m_EntityIdToCoordinate.ContainsKey(entity.id))
            {
                Coordinate current = m_EntityIdToCoordinate[entity.id];

                if (m_CoordinateToMapTile.ContainsKey(current))
                {
                    m_CoordinateToMapTile[current].entities.Remove(entity);
                }

                m_EntityIdToCoordinate.Remove(entity.id);
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

        public void SetupMap(string mapFilePath)
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

                    m_CoordinateToMapTile[coordinate] = StringToMapTile(lineMapText[j], coordinate);
                    MapTileAdded?.Invoke(this, new MapTileAddedArgs() { mapTile = m_CoordinateToMapTile[coordinate], coordinate = coordinate });
                }
            }
        }

        #region Map CSV Parsing

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

        #endregion

    }
}