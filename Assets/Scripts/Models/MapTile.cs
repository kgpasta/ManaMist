﻿using ManaMist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

namespace ManaMist.Models
{
    public class MapTile : ScriptableObject
    {
        public Terrain terrain;
        public Resource resource;
        public bool isHighlighted;
        public List<Entity> entities = new List<Entity>();
    }
}
