using System.Collections.Generic;
using ManaMist.Players;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.UI
{

    public class PlayerStateView : MonoBehaviour
    {
        public MapView mapView;
        public Player player;

        private void OnEnable()
        {
            player.OnStateChange += OnPlayerStateChange;
        }

        private void OnDisable()
        {
            player.OnStateChange -= OnPlayerStateChange;
        }

        private void OnPlayerStateChange(object sender, IPlayerState state)
        {
            if (state is SelectedState)
            {
                SelectedState selectedState = state as SelectedState;
                ShowPaths(selectedState.paths);
            }
        }

        private void ShowPaths(Dictionary<Coordinate, Path> paths)
        {
            foreach (KeyValuePair<Coordinate, Path> path in paths)
            {
                Debug.Log(path.Key);
                mapView.HighlightMapTile(path.Key, new UnityEngine.Color(0.2f, 0.2f, 0.2f, 1.0f));
            }
        }
    }
}