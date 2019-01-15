using ManaMist.Actions;
using ManaMist.Commands;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Controllers
{

    [CreateAssetMenu(menuName = "ManaMist/Command Controller")]
    public class CommandController : ScriptableObject
    {
        public MapController mapController;
        public TurnController turnController;
        public Player playerOne;
        public Player playerTwo;
        public Player activePlayer;

        private void OnEnable()
        {
            turnController.OnTurnStart += SetActivePlayer;
        }

        private void OnDisable()
        {
            turnController.OnTurnStart -= SetActivePlayer;
        }


        public void DoCommand(Command command)
        {
            command.Execute(mapController, turnController, activePlayer);
        }

        private void SetActivePlayer(object sender, TurnEventArgs args)
        {
            activePlayer = args.player;
        }
    }
}