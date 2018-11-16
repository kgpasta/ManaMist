using System;
using ManaMist.Commands;
using ManaMist.Managers;

namespace ManaMist.Processors
{
    public class CommandProcessor
    {

        private GameManager gameManager;

        public CommandProcessor(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void Process(Command command)
        {
            Console.WriteLine(command.ToString());
            gameManager.DoCommand(command);
        }
    }
}