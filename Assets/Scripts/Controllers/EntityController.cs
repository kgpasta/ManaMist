using System;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Models;
using ManaMist.UI;
using UnityEngine;

namespace ManaMist.Controllers
{

    [CreateAssetMenu(menuName = "ManaMist/Entity Controller")]
    public class EntityController : ScriptableObject
    {
        [Header("Default Prefab")]
        [SerializeField] private GameObject m_DefaultEntityPrefab;
        public List<Entity> entities = new List<Entity>();
        public List<GameObject> entityViews = new List<GameObject>();

        public Entity CreateEntity(EntityType entityType)
        {
            Entity entityOutline = entities.Find(entity => entity.type == entityType);
            return ScriptableObject.Instantiate(entityOutline);
        }

        public GameObject GetEntityPrefab(Entity entity)
        {
            GameObject entityPrefab = entityViews.Find(entityView => entityView.name.Equals(entity.type.ToString(), StringComparison.InvariantCultureIgnoreCase));
            return entityPrefab != null ? entityPrefab : m_DefaultEntityPrefab;
        }
    }
}
