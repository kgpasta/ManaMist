using System.Collections.Generic;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/Event Controller")]
    public class EventController : ScriptableObject
    {
        private const int CometDamage = 5;
        [SerializeField] private MapController mapController;
        [SerializeField] private TurnController turnController;

        private void OnEnable()
        {
            turnController.OnTurnEnd += OnTurnEnd;
        }

        private void OnDisable()
        {
            turnController.OnTurnEnd -= OnTurnEnd;
        }

        private void OnTurnEnd(object sender, TurnEventArgs args)
        {
            if (args.turnNumber % 6 == 0)
            {
                CreateManaComet();
            }
        }

        private void CreateManaComet()
        {
            Coordinate targetCoordinate = mapController.GetRandomCoordinate();
            MapTile targetTile = mapController.GetMapTileAtCoordinate(targetCoordinate);

            if (targetTile.resource == Resource.NONE)
            {
                mapController.ModifyTileResource(targetTile, Resource.MANA, targetCoordinate);
                List<Coordinate> coordinates = targetCoordinate.GetNeighbors();
                coordinates.Add(targetCoordinate);

                foreach (Coordinate coordinate in coordinates)
                {
                    MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
                    foreach (Entity entity in mapTile.entities)
                    {
                        entity.Hp = ManaMistMath.Clamp(entity.Hp - CometDamage, 0, entity.Hp);
                    }
                }
            }
            else
            {
                CreateManaComet();
            }
        }
    }
}