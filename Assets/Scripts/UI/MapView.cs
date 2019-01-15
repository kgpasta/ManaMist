using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class MapView : MonoBehaviour
    {
        [Header("Dispatcher")]
        public Dispatcher dispatcher;
        [Header("Controllers")]
        public MapController mapController;
        public EntityController entityController;

        [Header("MapTile Prefab Reference")]
        public GameObject MapTilePrefabReference = null;

        [Header("UI Elements")]
        public Transform MapGridParentTransform = null;

        private Dictionary<Coordinate, Transform> m_CoordinateToTransform = new Dictionary<Coordinate, Transform>();

        private void OnEnable()
        {
            mapController.MapTileAdded += AddMapTileToMap;
            mapController.EntityAdded += AddEntityModelToMap;
        }

        private void OnDisable()
        {
            mapController.MapTileAdded -= AddMapTileToMap;
            mapController.EntityAdded -= AddEntityModelToMap;
        }

        private void AddMapTileToMap(object sender, MapTileAddedArgs args)
        {
            GameObject newMapTileWidgetInstance = Instantiate(MapTilePrefabReference, MapGridParentTransform);
            newMapTileWidgetInstance.name = "Tile (" + args.coordinate.x.ToString() + "," + args.coordinate.y.ToString() + ")";

            newMapTileWidgetInstance.GetComponent<MapTileWidget>().mapTile = args.mapTile;

            newMapTileWidgetInstance.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectedStateData selectedStateData = ScriptableObject.CreateInstance<SelectedStateData>();
                selectedStateData.coordinate = args.coordinate;
                dispatcher.Dispatch<SelectedState>(selectedStateData);
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

    }
}