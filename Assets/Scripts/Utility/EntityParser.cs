using System;
using System.Collections.Generic;
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
                    if (!string.IsNullOrEmpty(entityText[j]))
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
            entity.m_Type = (EntityType)System.Enum.Parse(typeof(EntityType), fields[nameof(name)]);
            entity.maxActionPoints = System.Int32.Parse(fields[nameof(actionPoints)]);
            entity.maxHp = System.Int32.Parse(fields[nameof(hp)]);
            entity.hp = entity.maxHp;

            entity.m_Cost = ScriptableObject.CreateInstance<Cost>();
            entity.m_Cost.food = System.Int32.Parse(fields[nameof(entity.m_Cost.food)]);
            entity.m_Cost.metal = System.Int32.Parse(fields[nameof(entity.m_Cost.metal)]);
            entity.m_Cost.mana = System.Int32.Parse(fields[nameof(entity.m_Cost.mana)]);
            AssetDatabase.CreateAsset(entity.cost, "Assets/ScriptableObjects/Costs/Entities/" + entity.name + "Cost.asset");

            MapController mapController = AssetDatabase.LoadAssetAtPath<MapController>("Assets/ScriptableObjects/MapController.asset");
            if (fields.ContainsKey(nameof(MoveAction) + "." + nameof(MoveAction.movementRange)))
            {
                MoveAction moveAction = ParseMoveAction(mapController, fields);
                AssetDatabase.CreateAsset(moveAction, "Assets/ScriptableObjects/Actions/MoveActions/" + entity.name + "MoveAction.asset");
                entity.AddAction(moveAction);
            }
            if (fields.ContainsKey(nameof(AttackAction) + "." + nameof(AttackAction.attack)))
            {
                AttackAction attackAction = ParseAttackAction(mapController, fields);
                AssetDatabase.CreateAsset(attackAction, "Assets/ScriptableObjects/Actions/AttackActions/" + entity.name + "AttackAction.asset");
                entity.AddAction(attackAction);
            }

            return (Entity)entity;
        }

        private static List<T> ParseStringAsList<T>(string input) where T : IConvertible
        {
            List<T> newList = new List<T>();
            foreach (string splitString in input.Split(':'))
            {
                newList.Add((T)Enum.Parse(typeof(T), splitString));
            }

            return newList;
        }

        private static MoveAction ParseMoveAction(MapController mapController, Dictionary<string, string> fields)
        {
            MoveAction moveAction = ScriptableObject.CreateInstance<MoveAction>();
            moveAction.movementRange = System.Int32.Parse(fields[nameof(MoveAction) + "." + nameof(MoveAction.movementRange)]);
            moveAction.actionPoints = System.Int32.Parse(fields[nameof(MoveAction) + "." + nameof(actionPoints)]);
            moveAction.allowedTerrain = ParseStringAsList<Models.Terrain>(fields[nameof(MoveAction) + "." + nameof(MoveAction.allowedTerrain)]);
            moveAction.mapController = mapController;

            return moveAction;
        }

        private static AttackAction ParseAttackAction(MapController mapController, Dictionary<string, string> fields)
        {
            AttackAction attackAction = ScriptableObject.CreateInstance<AttackAction>();
            attackAction.actionPoints = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(actionPoints)]);
            attackAction.attack = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.attack)]);
            attackAction.defense = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.defense)]);
            attackAction.accuracy = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.accuracy)]);
            attackAction.speed = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.speed)]);
            attackAction.skill = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.skill)]);
            attackAction.range = System.Int32.Parse(fields[nameof(AttackAction) + "." + nameof(AttackAction.range)]);
            attackAction.mapController = mapController;

            return attackAction;
        }
    }
}