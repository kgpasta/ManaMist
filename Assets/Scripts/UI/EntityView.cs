using ManaMist.Models;
using UnityEngine;

namespace ManaMist.UI
{
    public class EntityView : MonoBehaviour
    {
        public Entity entity;

        public void Initialize(Entity entity)
        {
            this.entity = entity;
            this.gameObject.name = entity.name;
            this.GetComponent<MeshRenderer>().material.color = entity.color;
        }
    }
}