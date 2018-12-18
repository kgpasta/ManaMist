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

        [Header("MapTile Prefab Reference")]
        public GameObject MapTilePrefabReference = null;

        [Header("Model Prefabs")]
        public GameObject WorkerModel = null;
        public GameObject TownCenterModel = null;
        public GameObject MineModel = null;

        [Header("UI Elements")]
        public Transform MapGridParentTransform = null;

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
            newMapTileWidgetInstance.GetComponent<MapTileWidget>().mapTile.instanceReference = newMapTileWidgetInstance;

            newMapTileWidgetInstance.GetComponent<Button>().onClick.AddListener(() =>
            {
                commandController.MapTileSelected(args.coordinate);
            });
        }

        private void AddEntityModelToMap(object sender, EntityAddedArgs args)
        {
            GameObject parentTile = args.mapTile.instanceReference;

            if (args.entity is Worker)
            {
                args.entity.instanceReference = Instantiate(WorkerModel, parentTile.transform);
            }
            else if (args.entity is TownCenter)
            {
                args.entity.instanceReference = Instantiate(TownCenterModel, parentTile.transform);
            }
            else if (args.entity is Mine)
            {
                args.entity.instanceReference = Instantiate(MineModel, parentTile.transform);
            }
        }

    }
}