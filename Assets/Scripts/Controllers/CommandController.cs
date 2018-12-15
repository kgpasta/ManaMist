using ManaMist.Actions;
using ManaMist.Commands;
using ManaMist.Models;
using ManaMist.Players;
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
        public void DoCommand(Command command)
        {
            switch (command.type)
            {
                case CommandType.DESCRIBE:
                    DescribeCommand describeCommand = (DescribeCommand)command;
                    describeCommand.Execute(mapController, describeCommand.id != null ? FindEntity(describeCommand.id) : null);
                    break;
                case CommandType.SELECT:
                    SelectCommand selectCommand = (SelectCommand)command;
                    selectCommand.Execute(mapController, GetPlayerById(selectCommand.playerId));
                    break;
                case CommandType.PERFORMACTION:
                    PerformActionCommand<MoveAction> performActionCommand = (PerformActionCommand<MoveAction>)command;
                    performActionCommand.Execute(mapController, GetPlayerById(performActionCommand.playerId), GetPlayerById(performActionCommand.playerId).selectedEntity);
                    break;
                case CommandType.ENDTURN:
                    EndTurnCommand endTurnCommand = (EndTurnCommand)command;
                    endTurnCommand.Execute(turnController);
                    break;
                default:
                    break;
            }
        }

        private Entity FindEntity(string id)
        {
            Entity playerOneEntity = playerOne.GetEntity(id);
            Entity playerTwoEntity = playerTwo.GetEntity(id);

            if (playerOneEntity != null)
            {
                return playerOneEntity;
            }
            else if (playerTwoEntity != null)
            {
                return playerTwoEntity;
            }
            return null;
        }

        private Player GetPlayerById(int playerId)
        {
            return playerId == 1 ? playerOne : playerTwo;
        }
    }
}