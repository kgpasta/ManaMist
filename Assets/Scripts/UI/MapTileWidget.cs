using ManaMist.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapTileWidget : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private MapTile m_MapTile = null;
    public MapTile mapTile
    {
        get { return m_MapTile; }
        set
        {
            m_MapTile = value;
            SetResourceModel(m_MapTile.resource);
        }
    }

    private struct CometData
    {
        public Vector3 m_MeteorTargetPosition;
        public Transform m_MeteorTransform;

        public CometData (Transform meteorTransform, Vector3 meteorTargetPosition)
        {
            m_MeteorTransform = meteorTransform;
            m_MeteorTargetPosition = meteorTargetPosition;
        }
    }

    private List<CometData> m_CurrentAciveCometsList;

    [Header("UI Elements")]
    [SerializeField] private Image m_TileImage;
    [SerializeField] private GameObject m_HighlightTile;

    [Header("Prefab References")]
    [SerializeField] private GameObject m_ManaResourcePrefabReference;
    [SerializeField] private GameObject m_MetalResourcePrefabReference;
    [SerializeField] private GameObject m_FoodResourcePrefabReference;

    private GameObject m_CurrentMapTileModel = null;

    #region Events

    public class MapTileClickedEventArgs : EventArgs
    {
        public PointerEventData pointerEventData { get; }

        public MapTileClickedEventArgs(PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
        }
    }

    public event EventHandler<MapTileClickedEventArgs> MapTileClicked;

    #endregion

    private void OnEnable()
    {
        m_CurrentAciveCometsList = new List<CometData>();
    }

    private void Update()
    {
        UpdateComets();
    }

    private void UpdateComets()
    {
        //realized this is all overkill, each tile manages itself so don't need a list here...@todo to simplify
        List<int> indicesToRemove = new List<int>();
        foreach (CometData comet in m_CurrentAciveCometsList)
        {
            float step = 2.5f * Time.deltaTime; // calculate distance to move
            comet.m_MeteorTransform.position = Vector3.MoveTowards(comet.m_MeteorTransform.position, comet.m_MeteorTargetPosition, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(comet.m_MeteorTransform.position, comet.m_MeteorTargetPosition) < 0.001f)
            {
                // Swap the position of the cylinder.
                comet.m_MeteorTransform.position = comet.m_MeteorTargetPosition;
                indicesToRemove.Add(m_CurrentAciveCometsList.IndexOf(comet));
                m_CurrentMapTileModel.GetComponent<ParticleSystem>().Stop();
            }
        }

        foreach (int index in indicesToRemove)
        {
            m_CurrentAciveCometsList.RemoveAt(index);
        }
    }

    private void OnGUI()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (m_MapTile != null)
        {
            m_TileImage.color = MassiveShittyColorSwitchStatement();
            m_HighlightTile.SetActive(m_MapTile.isHighlighted);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MapTileClicked.Invoke(this, new MapTileClickedEventArgs(eventData));
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

    public void SetResourceModel(Resource resource)
    {
        if (m_CurrentMapTileModel != null)
        {
            Destroy(m_CurrentMapTileModel);
        }

        switch (resource)
        {
            case Resource.FOOD:

                m_CurrentMapTileModel = Instantiate(m_FoodResourcePrefabReference, transform);
                break;

            case Resource.MANA:

                m_CurrentMapTileModel = Instantiate(m_ManaResourcePrefabReference, transform);
                m_CurrentMapTileModel.GetComponent<ParticleSystem>().Stop();
                break;

            case Resource.METAL:

                m_CurrentMapTileModel = Instantiate(m_MetalResourcePrefabReference, this.transform);
                break;

            default:
                break;
        }
    }

    public void InitiateManaComet()
    {
        m_CurrentMapTileModel = Instantiate(m_ManaResourcePrefabReference, this.transform);
        Vector3 targetPosition = m_CurrentMapTileModel.transform.position;
        m_CurrentMapTileModel.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4.0f, this.transform.position.z);
        m_CurrentMapTileModel.GetComponent<ParticleSystem>().Play();
        m_CurrentAciveCometsList.Add(new CometData(m_CurrentMapTileModel.transform, targetPosition));
    }
}
