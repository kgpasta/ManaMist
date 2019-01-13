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
        public List<EntityView> entityViews;

        public GameObject GetEntityPrefab(Entity entity)
        {
            return entityViews.Find(entityView => entity.GetType() == entityView.entity.GetType()).gameObject;
        }
    }
}
