﻿using ManaMist.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ManaMist.Models
{
    public class MapTile : ScriptableObject
    {
        public Terrain terrain;
        public Resource resource;
        public List<Entity> entities;
        public event EventHandler PropertyChanged;

        private void OnValidate()
        {
            Debug.Log("On validate");
            PropertyChanged?.Invoke(this, null);
        }
    }
}
