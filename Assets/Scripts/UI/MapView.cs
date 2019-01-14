using System.Collections.Generic;
using ManaMist.Commands;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class MapView : MonoBehaviour
    {
        public CommandController commandController;
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
                commandController.MapTileSelected(args.coordinate);
            });

            m_CoordinateToTransform.Add(args.coordinate, newMapTileWidgetInstance.transform);
        }

        private void AddEntityModelToMap(object sender, EntityAddedArgs args)
        {
            GameObject entityPrefab = entityController.GetEntityPrefab(args.entity);
            Instantiate(entityPrefab, m_CoordinateToTransform[args.coordinate]);
        }

        public void HighlightMapTile(Coordinate coordinate)
        {
            MapTile mapTile = m_CoordinateToTransform[coordinate].gameObject.GetComponent<MapTile>();
            mapTile.isHighlighted = true;
        }

    }
}