using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/SeedController")]
    public class SeedController : ScriptableObject
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private EntityController entityController;
        public EntityType townCenterType;
        public EntityType workerType;
        public EntityType warriorType;

        public void SeedPlayer(Player player, int offset)
        {
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            Entity townCenter = entityController.CreateEntity(townCenterType);
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate warriorCoordinate = new Coordinate(offset, offset + 1);
            Entity warrior = entityController.CreateEntity(warriorType);
            player.AddEntity(warrior);
            mapController.AddToMap(warriorCoordinate, warrior);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            Entity worker = entityController.CreateEntity(workerType);
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }
    }
}