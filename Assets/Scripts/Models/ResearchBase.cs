using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Models
{
    public class ResearchBase : ScriptableObject
    {
        public Cost cost;
        public List<ResearchBase> prerequesites;
    }
}