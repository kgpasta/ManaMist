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
            entity.color = this.color;
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);

            if (entity?.id == victoryConditionEntity.id)
            {
                VictoryConditionEntityRemoved?.Invoke(this, EventArgs.Empty);
            }
        }

        public Entity GetEntity(string id)
        {
            return entities.Find(entity => entity.id == id);
        }

        private void OnDisable()
        {
            entities = new List<Entity>();
            resources.food = 100;
            resources.mana = 100;
            resources.metal = 100;
        }

    }
}
