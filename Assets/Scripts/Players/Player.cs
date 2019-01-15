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
        public Cost resources;

        public void InitializeTurn()
        {
            IncrementResources();

            Debug.Log("Player " + id + " has " + resources.ToString());
        }

        private void IncrementResources()
        {
            foreach (Entity entity in entities)
            {
                HarvestAction action = entity.GetAction<HarvestAction>();

                if (action != null)
                {
                    resources.Increment(action.harvestAmount);
                }

            }
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public Entity GetEntity(string id)
        {
            return entities.Find(entity => entity.id == id);
        }

        private void OnDisable()
        {
            entities = new List<Entity>();
        }

    }
}
