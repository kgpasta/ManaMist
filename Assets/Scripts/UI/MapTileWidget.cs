using ManaMist.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTileWidget : MonoBehaviour {

    public MapTile mapTile = null;

    [Header("UI Elements")]
    [SerializeField] private Image m_TileImage;

    private void OnGUI()
    {
        UpdateUI();
    }

    private void UpdateUI()
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

                return new Color(255, 214, 127);

            case ManaMist.Models.Terrain.FOREST:

                return new Color(8, 89, 0);

            case ManaMist.Models.Terrain.GRASS:

                return new Color(108, 226, 95);

            case ManaMist.Models.Terrain.HILL:

                return new Color(21, 214, 0);

            case ManaMist.Models.Terrain.MOUNTAIN:

                return new Color(91, 91, 91);

            case ManaMist.Models.Terrain.SWAMP:

                return new Color(69, 91, 18);

            case ManaMist.Models.Terrain.WATER:

                return new Color(66, 212, 244);

            default:

                return new Color(0, 0, 0);

        }
    }
}
