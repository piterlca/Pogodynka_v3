using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogodynka_v3
{
    public abstract class Controller
    {
        protected List<string> availableModels = new List<string>();
        protected View ViewUsed;

        private int COMMAND_NAME = 0;
        private int COMMAND_ARGUMENT = 1;

        protected void executeCommand(string command)
        {
            string[] CommandStructure = processCommand(command);
            Model ModelUsed = Model.identifyModel(CommandStructure[COMMAND_ARGUMENT]);

            switch (CommandStructure[COMMAND_NAME])
            {
                case "ADD":
                    ModelUsed.addSubscriber(ViewUsed);
                    break;

                case "DEL":
                    ModelUsed.delSubscriber(ViewUsed);
                    ViewUsed.delDataFromView(CommandStructure[COMMAND_ARGUMENT]);
                    break;
            }
        }

        private string[] processCommand(string command)
        {
            string[] CommandStruct = new string[2];
            foreach (char letterOfCommand in command)
            {
                if (System.Char.IsUpper(letterOfCommand))
                {
                    CommandStruct[COMMAND_NAME] += letterOfCommand;
                }
                else
                {
                    CommandStruct[COMMAND_ARGUMENT] += letterOfCommand;
                }
            }

            CommandStruct[COMMAND_ARGUMENT] = tools.TextTools.StartWithCapital(CommandStruct[COMMAND_ARGUMENT]);
            return CommandStruct;
        }

    }
}
