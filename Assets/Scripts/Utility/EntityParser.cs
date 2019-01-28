using System;
using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using UnityEditor;
using UnityEngine;

namespace ManaMist.Utility
{
    public class EntityParser : Entity
    {
        public static void ReadEntityCsv(string path)
        {
            UnityEngine.Object entitiesObject = Resources.Load(path);
            TextAsset entitiesText = entitiesObject as TextAsset;

            string[] allEntitiesText = entitiesText.text.Split('\n');
            string[] fieldText = allEntitiesText[0].Split(',');

            EntityController entityController = AssetDatabase.LoadAssetAtPath<EntityController>("Assets/ScriptableObjects/EntityController.asset");
            entityController.entities.Clear();
            for (int i = 1; i < allEntitiesText.Length; i++)
            {
                Dictionary<string, string> entityDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                string[] entityText = allEntitiesText[i].Split(',');
                for (int j = 0; j < entityText.Length; j++)
                {
                    if (!string.IsNullOrWhiteSpace(entityText[j]))
                    {
                        entityDict.Add(fieldText[j].Trim(), entityText[j].Trim());
                    }
                }

                Entity entity = ParseEntity(entityDict);
                AssetDatabase.CreateAsset(entity, "Assets/ScriptableObjects/Entities/" + entity.name + ".asset");
                entityController.entities.Add(entity);
            }

            Debug.Log(allEntitiesText.Length - 1 + " entities added");
        }

        public static Entity ParseEntity(Dictionary<string, string> fields)
        {
            EntityParser entity = ScriptableObject.CreateInstance<EntityParser>();
            entity.name = fields[nameof(name)];
            entity.m_Type = (EntityType)Enum.Parse(typeof(EntityType), fields[nameof(name)], true);
            entity.maxActionPoints = Int32.Parse(fields[nameof(actionPoints)]);
            entity.maxHp = Int32.Parse(fields[nameof(hp)]);
            entity.hp = entity.maxHp;

            entity.m_Cost = ParseCost(fields);
            AssetDatabase.CreateAsset(entity.cost, "Assets/ScriptableObjects/Costs/Entities/" + entity.name + "Cost.asset");

            MapController mapController = AssetDatabase.LoadAssetAtPath<MapController>("Assets/ScriptableObjects/MapController.asset");
            if (fields.Keys.Any(field => field.Contains(nameof(MoveAction))))
            {
                MoveAction moveAction = ParseMoveAction(mapController, fields);
                AssetDatabase.CreateAsset(moveAction, "Assets/ScriptableObjects/Actions/MoveActions/" + entity.name + "MoveAction.asset");
                entity.AddAction(moveAction);
            }
            if (fields.Keys.Any(field => field.Contains(nameof(AttackAction))))
            {
                AttackAction attackAction = ParseAttackAction(mapController, fields);
                AssetDatabase.CreateAsset(attackAction, "Assets/ScriptableObjects/Actions/AttackActions/" + entity.name + "AttackAction.asset");
                entity.AddAction(attackAction);
            }
            if (fields.Keys.Any(field => field.Contains(nameof(BuildAction))))
            {
                BuildAction buildAction = ParseBuildAction(mapController, fields);
                AssetDatabase.CreateAsset(buildAction, "Assets/ScriptableObjects/Actions/BuildActions/" + entity.name + "BuildAction.asset");
                entity.AddAction(buildAction);
            }
            if (fields.Keys.Any(field => field.Contains(nameof(HarvestAction))))
            {
                HarvestAction harvestAction = ParseHarvestAction(entity, mapController, fields);
                AssetDatabase.CreateAsset(harvestAction, "Assets/ScriptableObjects/Actions/HarvestActions/" + entity.name + "HarvestAction.asset");
                entity.AddAction(harvestAction);
            }

            return (Entity)entity;
        }

        private static List<T> ParseStringAsList<T>(string input) where T : IConvertible
        {
            List<T> newList = new List<T>();
            foreach (string splitString in input.Split(':'))
            {
                newList.Add((T)Enum.Parse(typeof(T), splitString, true));
            }

            return newList;
        }

        private static Cost ParseCost(Dictionary<string, string> fields, string prefix = "")
        {
            Cost cost = ScriptableObject.CreateInstance<Cost>();
            cost.food = Int32.Parse(fields[prefix + nameof(Cost.food)]);
            cost.metal = Int32.Parse(fields[prefix + nameof(Cost.metal)]);
            cost.mana = Int32.Parse(fields[prefix + nameof(Cost.mana)]);
            return cost;
        }

        private static T ParseAction<T>(MapController mapController, Dictionary<string, string> fields) where T : Actions.Action
        {
            T action = ScriptableObject.CreateInstance<T>();
            action.mapController = mapController;
            action.actionPoints = Int32.Parse(fields[typeof(T).Name + "." + nameof(actionPoints)]);

            return action;
        }

        private static MoveAction ParseMoveAction(MapController mapController, Dictionary<string, string> fields)
        {
            MoveAction moveAction = ParseAction<MoveAction>(mapController, fields);
            moveAction.movementRange = Int32.Parse(fields[nameof(MoveAction) + "." + nameof(MoveAction.movementRange)]);
            moveAction.allowedTerrain = ParseStringAsList<Models.Terrain>(fields[nameof(MoveAction) + "." + nameof(MoveAction.allowedTerrain)]);

            return moveAction;
        }

        private static AttackAction ParseAttackAction(MapController mapController, Dictionary<string, string> fields)
        {
            AttackAction attackAction = ParseAction<AttackAction>(mapController, fields);

            attackAction.attack = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.attack)]);
            attackAction.defense = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.defense)]);
            attackAction.accuracy = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.accuracy)]);
            attackAction.speed = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.speed)]);
            attackAction.skill = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.skill)]);
            attackAction.range = Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.range)]);

            return attackAction;
        }

        private static BuildAction ParseBuildAction(MapController mapController, Dictionary<string, string> fields)
        {
            BuildAction buildAction = ParseAction<BuildAction>(mapController, fields);
            buildAction.canBuildList = ParseStringAsList<EntityType>(fields[nameof(BuildAction) + "." + nameof(BuildAction.canBuildList)]);

            return buildAction;
        }

        private static HarvestAction ParseHarvestAction(Entity entity, MapController mapController, Dictionary<string, string> fields)
        {
            HarvestAction harvestAction = ParseAction<HarvestAction>(mapController, fields);
            harvestAction.resource = (Resource)Enum.Parse(typeof(Resource), fields[nameof(HarvestAction) + "." + nameof(HarvestAction.resource)], true);
            harvestAction.harvestAmount = ParseCost(fields, nameof(HarvestAction) + ".");
            AssetDatabase.CreateAsset(harvestAction.harvestAmount, "Assets/ScriptableObjects/Costs/Harvests/" + entity.name + "HarvestCost.asset");
            return harvestAction;
        }
    }
}