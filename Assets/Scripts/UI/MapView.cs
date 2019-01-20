using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class MapView : MonoBehaviour
    {
        [Header("Controllers")]
        public MapController mapController;
        public EntityController entityController;
        public InputController inputController;

        [Header("MapTile Prefab Reference")]
        public GameObject MapTilePrefabReference = null;

        [Header("UI Elements")]
        public Transform MapGridParentTransform = null;

        private Dictionary<Coordinate, Transform> m_CoordinateToTransform = new Dictionary<Coordinate, Transform>();

        private void OnEnable()
        {
            mapController.MapTileAdded += AddMapTileToMap;
            mapController.EntityAdded += AddEntityModelToMap;
            mapController.EntityMoved += MoveEntityModel;
        }

        private void OnDisable()
        {
            mapController.MapTileAdded -= AddMapTileToMap;
            mapController.EntityAdded -= AddEntityModelToMap;
            mapController.EntityMoved -= MoveEntityModel;
        }

        private void AddMapTileToMap(object sender, MapTileAddedArgs args)
        {
            GameObject newMapTileWidgetInstance = Instantiate(MapTilePrefabReference, MapGridParentTransform);
            newMapTileWidgetInstance.name = "Tile (" + args.coordinate.x.ToString() + "," + args.coordinate.y.ToString() + ")";

            newMapTileWidgetInstance.GetComponent<MapTileWidget>().mapTile = args.mapTile;

            newMapTileWidgetInstance.GetComponent<Button>().onClick.AddListener(() =>
            {
                MapTileClickedInput mapTileClickedInput = new MapTileClickedInput()
                {
                    coordinate = args.coordinate,
                    mapTile = args.mapTile
                };
                inputController.RegisterInputEvent(mapTileClickedInput);
            });

            m_CoordinateToTransform.Add(args.coordinate, newMapTileWidgetInstance.transform);
        }

        private void AddEntityModelToMap(object sender, EntityAddedArgs args)
        {
            GameObject entityPrefab = entityController.GetEntityPrefab(args.entity);
            GameObject entityInstance = Instantiate(entityPrefab, m_CoordinateToTransform[args.coordinate]);
            entityInstance.name = args.entity.name;
            entityInstance.GetComponent<EntityView>().entity = args.entity;
        }

        private void MoveEntityModel(object sender, EntityMovedArgs args)
        {
            Transform transform = m_CoordinateToTransform[args.previousCoordinate].GetComponentInChildren<EntityView>().transform;
            transform.SetParent(m_CoordinateToTransform[args.coordinate], false);
        }

    }
}