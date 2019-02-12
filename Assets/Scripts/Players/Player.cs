using System;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Players
{
    [CreateAssetMenu(menuName = "ManaMist/Player")]
    public class Player : ScriptableObject
    {
        public int id;
        public List<Entity> entities = new List<Entity>();
        public Entity victoryConditionEntity;
        public Cost resources;
        public Behavior behavior;
        public Color color;
        public List<ResearchBase> research;

        public event EventHandler VictoryConditionEntityRemoved;

        public void InitializeTurn()
        {
            foreach (Entity entity in entities)
            {
                entity.ResetActionPoints();
                entity.PerformTurnStartActions(this);
            }
            behavior?.OnTurnStart();
        }

        public void AddEntity(Entity entity)
        {
            entity.Color = this.color;
            entities.Add(entity);
            entity.EntityKilled += RemoveEntity;
        }

        public void RemoveEntity(Entity entity)
        {
            entity.EntityKilled -= RemoveEntity;
            entities.Remove(entity);

            if (entity?.Id == victoryConditionEntity.Id)
            {
                VictoryConditionEntityRemoved?.Invoke(this, EventArgs.Empty);
            }
        }

        public Entity GetEntity(string id)
        {
            return entities.Find(entity => entity.Id == id);
        }

        private void OnDisable()
        {
            research.Clear();
            entities.Clear();
            resources.food = 100;
            resources.mana = 100;
            resources.metal = 100;
        }

    }
}
