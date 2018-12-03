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
        get { return m_MapTile; }
        set
        {
            m_MapTile = value;
        }
    }

    [Header("UI Elements")]
    [SerializeField] private Image m_TileImage;

    private void OnGUI()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (m_MapTile != null)
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

                return new Color32(67, 147, 65, 255);

            case ManaMist.Models.Terrain.HILL:

                return new Color32(58, 68, 59, 255);

            case ManaMist.Models.Terrain.MOUNTAIN:

                return new Color32(112, 112, 112, 255);

            case ManaMist.Models.Terrain.SWAMP:

                return new Color32(127, 77, 165, 255);

            case ManaMist.Models.Terrain.WATER:

                return new Color32(7, 106, 193, 255);

            default:

                return new Color32(0, 0, 0, 255);

        }
    }
}
