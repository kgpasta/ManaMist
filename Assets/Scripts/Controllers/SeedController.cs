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

        public void SeedPlayer(Player player, int offset)
        {
            player.color = new Color(Random.value, Random.value, Random.value);
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            EntityType townCenterType = ScriptableObject.CreateInstance<EntityType>();
            townCenterType.Name = "TownCenter";
            Entity townCenter = entityController.CreateEntity(townCenterType);
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate warriorCoordinate = new Coordinate(offset, offset + 1);
            EntityType warriorType = ScriptableObject.CreateInstance<EntityType>();
            warriorType.Name = "Warrior";
            Entity warrior = entityController.CreateEntity(warriorType);
            player.AddEntity(warrior);
            mapController.AddToMap(warriorCoordinate, warrior);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            EntityType workerType = ScriptableObject.CreateInstance<EntityType>();
            workerType.Name = "Worker";
            Entity worker = entityController.CreateEntity(workerType);
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }
    }
}