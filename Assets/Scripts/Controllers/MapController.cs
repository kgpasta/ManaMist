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

    public class EntityArgs
    {
        public Entity entity;
        public Coordinate coordinate;
    }

    public class EntityMovedArgs : EntityArgs
    {
        public Coordinate previousCoordinate;
    }

    public class MaptTileModifiedArgs
    {
        public MapTile mapTile;
        public Coordinate coordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/Map Controller")]
    public class MapController : ScriptableObject
    {
        public event EventHandler<MapTileAddedArgs> MapTileAdded;
        public event EventHandler<EntityArgs> EntityAdded;
        public event EventHandler<EntityMovedArgs> EntityMoved;
        public event EventHandler<EntityArgs> EntityRemoved;
        public event EventHandler<MaptTileModifiedArgs> MapTileModified;
        private Dictionary<Coordinate, MapTile> m_CoordinateToMapTile = new Dictionary<Coordinate, MapTile>();
        private Dictionary<string, Coordinate> m_EntityIdToCoordinate = new Dictionary<string, Coordinate>();

        public const int MAP_DIMENSION = 25;

        public void AddToMap(Coordinate coordinate, Entity entity)
        {
            MapTile mapTile = m_CoordinateToMapTile[coordinate];

            mapTile.entities.Add(entity);

            m_EntityIdToCoordinate[entity.Id] = coordinate;

            entity.EntityKilled += RemoveFromMap;

            EntityAdded?.Invoke(this, new EntityArgs()
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
            if (m_EntityIdToCoordinate.ContainsKey(entity.Id))
            {
                entity.EntityKilled -= RemoveFromMap;
                Coordinate current = m_EntityIdToCoordinate[entity.Id];

                if (m_CoordinateToMapTile.ContainsKey(current))
                {
                    m_CoordinateToMapTile[current].entities.Remove(entity);
                }

                m_EntityIdToCoordinate.Remove(entity.Id);

                EntityRemoved?.Invoke(this, new EntityArgs()
                {
                    entity = entity,
                    coordinate = current
                });
            }
        }

        public void MoveEntity(Coordinate coordinate, Entity entity)
        {
            if (GetPositionOfEntity(entity.Id) != null)
            {
                // Remove from old coordinate
                Coordinate oldCoordinate = m_EntityIdToCoordinate[entity.Id];
                m_CoordinateToMapTile[oldCoordinate].entities.Remove(entity);

                //Add to new coordinate
                m_EntityIdToCoordinate[entity.Id] = coordinate;
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

        public void ModifyTileResource(MapTile targetTile, Resource newResource, Coordinate targetCoordinate)
        {
            targetTile.resource = newResource;
            MapTileModified?.Invoke(this, new MaptTileModifiedArgs() { mapTile = targetTile, coordinate = targetCoordinate });
        }

        public Coordinate GetRandomCoordinate()
        {
            return new Coordinate(UnityEngine.Random.Range(1, MAP_DIMENSION - 1), UnityEngine.Random.Range(1, MAP_DIMENSION - 1));
        }
    }
}