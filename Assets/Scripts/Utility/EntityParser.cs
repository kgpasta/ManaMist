using System;
using System.Collections.Generic;
using ManaMist.Actions;
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

            for (int i = 1; i < allEntitiesText.Length; i++)
            {
                Dictionary<string, string> entityDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                string[] entityText = allEntitiesText[i].Split(',');
                for (int j = 0; j < entityText.Length; j++)
                {
                    entityDict.Add(fieldText[j], entityText[j]);
                }

                Entity entity = ParseEntity(entityDict);
                AssetDatabase.CreateAsset(entity, "Assets/ScriptableObjects/Entities/" + entity.name + ".asset");
                AssetDatabase.CreateAsset(entity.cost, "Assets/ScriptableObjects/Costs/Entities/" + entity.name + "Cost.asset");
                AssetDatabase.CreateAsset(entity.actions[0], "Assets/ScriptableObjects/Actions/" + entity.name + "MoveAction.asset");
            }

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

            if (fields.ContainsKey(nameof(MoveAction) + "." + nameof(MoveAction.movementRange)))
            {
                MoveAction moveAction = ScriptableObject.CreateInstance<MoveAction>();
                moveAction.movementRange = System.Int32.Parse(fields[nameof(MoveAction) + "." + nameof(MoveAction.movementRange)]);
                moveAction.actionPoints = System.Int32.Parse(fields[nameof(MoveAction) + "." + nameof(actionPoints)]);
                moveAction.allowedTerrain = ParseStringAsList<Models.Terrain>(fields[nameof(MoveAction) + "." + nameof(MoveAction.allowedTerrain)]);
                entity.AddAction(moveAction);
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
    }
}