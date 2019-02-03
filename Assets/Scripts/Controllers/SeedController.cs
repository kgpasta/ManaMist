using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/SeedController")]
    public class SeedController : ScriptableObject
    {
        [SerializeField] private MapController m_MapController;
        [SerializeField] private EntityController m_EntityController;

        [SerializeField] private List<Color> m_PlayerColors;

        [Header("Player 1 Seeding Options")]
        [SerializeField] private Coordinate m_PlayerOneTownCenterCoordinate;
        [SerializeField] private Coordinate m_PlayerOneWorkerCoordinate;
        [SerializeField] private Coordinate m_PlayerOneWarriorCoordinate;

        [Header("Player 2 Seeding Options")]
        [SerializeField] private Coordinate m_PlayerTwoTownCenterCoordinate;
        [SerializeField] private Coordinate m_PlayerTwoWorkerCoordinate;
        [SerializeField] private Coordinate m_PlayerTwoWarriorCoordinate;

        public void SeedPlayer(Player player)
        {
            Coordinate townCenterCoordinate = new Coordinate();
            Coordinate warriorCoordinate = new Coordinate();
            Coordinate workerCoordinate = new Coordinate();

            // NOTE: Temporarily hard-coded for 2 players
            switch (player.name)
            {
                case "PlayerOne":
                    player.color = m_PlayerColors[0];
                    townCenterCoordinate = m_PlayerOneTownCenterCoordinate;
                    workerCoordinate = m_PlayerOneWorkerCoordinate;
                    warriorCoordinate = m_PlayerOneWarriorCoordinate;
                    break;

                case "PlayerTwo":
                    player.color = m_PlayerColors[1];
                    townCenterCoordinate = m_PlayerTwoTownCenterCoordinate;
                    workerCoordinate = m_PlayerTwoWorkerCoordinate;
                    warriorCoordinate = m_PlayerTwoWarriorCoordinate;
                    break;
            }

            EntityType townCenterType = ScriptableObject.CreateInstance<EntityType>();
            townCenterType.Name = "TownCenter";
            Entity townCenter = m_EntityController.CreateEntity(townCenterType);
            player.victoryConditionEntity = townCenter;
            player.AddEntity(townCenter);
            m_MapController.AddToMap(townCenterCoordinate, townCenter);

            EntityType warriorType = ScriptableObject.CreateInstance<EntityType>();
            warriorType.Name = "Warrior";
            Entity warrior = m_EntityController.CreateEntity(warriorType);
            player.AddEntity(warrior);
            m_MapController.AddToMap(warriorCoordinate, warrior);

            EntityType workerType = ScriptableObject.CreateInstance<EntityType>();
            workerType.Name = "Worker";
            Entity worker = m_EntityController.CreateEntity(workerType);
            player.AddEntity(worker);
            m_MapController.AddToMap(workerCoordinate, worker);
        }
    }
}