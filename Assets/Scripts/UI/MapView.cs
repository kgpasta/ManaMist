using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.UI
{
    public class MapView : MonoBehaviour
    {
        public MapController mapController;

        [Header("Prefab References")]
        public GameObject MapTilePrefabReference = null;

        [Header("UI Elements")]
        public Transform MapGridParentTransform = null;

        private void OnEnable()
        {
            mapController.MapTileAdded += AddMapTileToMap;
        }

        private void OnDisable()
        {
            mapController.MapTileAdded += AddMapTileToMap;
        }

        private void AddMapTileToMap(object sender, MapTileAddedArgs args)
        {
            GameObject newMapTileWidgetInstance = Instantiate(MapTilePrefabReference, MapGridParentTransform);
            newMapTileWidgetInstance.name = "Tile (" + args.coordinate.x.ToString() + "," + args.coordinate.y.ToString() + ")";

            newMapTileWidgetInstance.GetComponent<MapTileWidget>().mapTile = args.mapTile;
        }

    }
}