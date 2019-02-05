using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Research")]
    public class Research : ScriptableObject
    {
        public Cost cost;
        public List<Research> prerequesites;
    }
}