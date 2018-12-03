using ManaMist.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileWidget : MonoBehaviour
{
    [SerializeField]
    private MapTile m_MapTile = null;

    public MapTile mapTile
    {
        get
        {
            return m_MapTile;
        }
        set
        {
            m_MapTile = value;
            m_MapTile.PropertyChanged += UpdateUI;
        }
    }

    [Header("UI Elements")]
    [SerializeField] private Image m_TileImage;

    private void UpdateUI(object sender, EventArgs e)
    {
        if (mapTile != null)
        {
            m_TileImage.color = MassiveShittyColorSwitchStatement();
        }

    }

    private Color MassiveShittyColorSwitchStatement()
    {
        switch (mapTile.terrain)
        {
            case ManaMist.Models.Terrain.DESERT:

                return new Color32(255, 214, 127, 255);

            case ManaMist.Models.Terrain.FOREST:

                return new Color32(8, 89, 0, 255);

            case ManaMist.Models.Terrain.GRASS:

                return new Color32(108, 226, 95, 255);

            case ManaMist.Models.Terrain.HILL:

                return new Color32(21, 214, 0, 255);

            case ManaMist.Models.Terrain.MOUNTAIN:

                return new Color32(91, 91, 91, 255);

            case ManaMist.Models.Terrain.SWAMP:

                return new Color32(69, 91, 18, 255);

            case ManaMist.Models.Terrain.WATER:

                return new Color32(66, 212, 244, 255);

            default:

                return new Color32(0, 0, 0, 255);

        }
    }
}
