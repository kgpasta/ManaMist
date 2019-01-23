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

    public class EntityMovedArgs
    {
        public Entity entity;
        public Coordinate coordinate;
        public Coordinate previousCoordinate;
    }

    public class EntityRemovedArgs
    {
        public Entity entity;
        public Coordinate coordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/Map Controller")]
    public class MapController : ScriptableObject
    {
        public event EventHandler<MapTileAddedArgs> MapTileAdded;
        public event EventHandler<EntityAddedArgs> EntityAdded;
        public event EventHandler<EntityMovedArgs> EntityMoved;
        public event EventHandler<EntityRemovedArgs> EntityRemoved;
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

                EntityRemoved?.Invoke(this, new EntityRemovedArgs()
                {
                    entity = entity,
                    coordinate = current
                });
            }
        }

        public void MoveEntity(Coordinate coordinate, Entity entity)
        {
            if (GetPositionOfEntity(entity.id) != null)
            {
                // Remove from old coordinate
                Coordinate oldCoordinate = m_EntityIdToCoordinate[entity.id];
                m_CoordinateToMapTile[oldCoordinate].entities.Remove(entity);

                //Add to new coordinate
                m_EntityIdToCoordinate[entity.id] = coordinate;
                m_CoordinateToMapTile[coordinate].entities.Add(entity);

                EntityMoved?.Invoke(this, new EntityMovedArgs()
                {
                    previousCoordinate = oldCoordinate,
                    coordinate = coordinate,
                    entity = entity
                });
            }
        }

        public void SetupMap(string mapFilePath)
        {
            m_CoordinateToMapTile = new MapParser().SetupMap(mapFilePath, MAP_DIMENSION);

            foreach (KeyValuePair<Coordinate, MapTile> pair in m_CoordinateToMapTile)
            {
                MapTileAdded?.Invoke(this, new MapTileAddedArgs() { mapTile = pair.Value, coordinate = pair.Key });
            }
        }
    }
}