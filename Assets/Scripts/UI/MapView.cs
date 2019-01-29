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

        [Header("Entity Inspector Reference")]
        [SerializeField] private EntityInspectorPanel m_EntityInspector = null;

        private Dictionary<Coordinate, Transform> m_CoordinateToTransform = new Dictionary<Coordinate, Transform>();

        private void OnEnable()
        {
            mapController.MapTileAdded += AddMapTileToMap;
            mapController.EntityAdded += AddEntityModelToMap;
            mapController.EntityMoved += MoveEntityModel;
            mapController.EntityRemoved += RemoveEntityModelFromMap;
        }

        private void OnDisable()
        {
            mapController.MapTileAdded -= AddMapTileToMap;
            mapController.EntityAdded -= AddEntityModelToMap;
            mapController.EntityMoved -= MoveEntityModel;
            mapController.EntityRemoved -= RemoveEntityModelFromMap;
        }

        private void AddMapTileToMap(object sender, MapTileAddedArgs args)
        {
            GameObject newMapTileWidgetInstance = Instantiate(MapTilePrefabReference, transform);
            newMapTileWidgetInstance.name = "Tile (" + args.coordinate.x.ToString() + "," + args.coordinate.y.ToString() + ")";

            MapTileWidget mapTileWidget = newMapTileWidgetInstance.GetComponent<MapTileWidget>();

            mapTileWidget.mapTile = args.mapTile;

            mapTileWidget.MapTileClicked += delegate (object sentBy, MapTileWidget.MapTileClickedEventArgs e)
            {
                switch (e.pointerEventData.button)
                {
                    case UnityEngine.EventSystems.PointerEventData.InputButton.Left:

                        MapTileClickedInput mapTileClickedInput = new MapTileClickedInput()
                        {
                            coordinate = args.coordinate,
                            mapTile = args.mapTile
                        };
                        inputController.RegisterInputEvent(mapTileClickedInput);

                        break;

                    case UnityEngine.EventSystems.PointerEventData.InputButton.Right:

                        ShowEntityInspectorCanvas(mapTileWidget);

                        break;

                    default:

                        break;
                }
            };

            m_CoordinateToTransform.Add(args.coordinate, newMapTileWidgetInstance.transform);
        }

        private void AddEntityModelToMap(object sender, EntityArgs args)
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

        private void RemoveEntityModelFromMap(object sender, EntityArgs args)
        {
            Transform transform = m_CoordinateToTransform[args.coordinate];
            Destroy(transform.GetComponentInChildren<EntityView>().gameObject);
        }

        private void ShowEntityInspectorCanvas(MapTileWidget mapTileWidget)
        {
            if (mapTileWidget.mapTile.entities.Count > 0)
            {
                m_EntityInspector.entity = mapTileWidget.mapTile.entities[0];
                m_EntityInspector.gameObject.SetActive(true);
            }
            else
            {
                m_EntityInspector.gameObject.SetActive(false);
            }
        }

    }
}