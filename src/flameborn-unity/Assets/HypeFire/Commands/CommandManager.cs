using System.Collections.Generic;
using UnityEngine;

namespace HF.Commands
{
    public class CommandManager<T>
    {
        public int CurrentCommandIndex { get; protected set; } = 0;
        public int CommandCount { get; private set; } = 0;

        [field: SerializeField]
        public List<Command<T>> Commands { get; protected set; } = new List<Command<T>>();

        public void AddCommand(Command<T> command)
        {
            Commands.Add(command);
            CommandCount++;
        }

        public void ExecuteCommands(T controller)
        {
            while (CurrentCommandIndex >= 0 && CurrentCommandIndex < CommandCount)
            {
                if (Commands[CurrentCommandIndex].Execute(controller))
                {
                    CurrentCommandIndex++;
                }
            }
        }

        public void UndoCommands(T controller)
        {
            while (CurrentCommandIndex <= CommandCount && CurrentCommandIndex > 0)
            {
                if (Commands[CurrentCommandIndex - 1].Execute(controller))
                {
                    CurrentCommandIndex--;
                }
            }
        }

        public void Clear()
        {
            Commands.Clear();
            CurrentCommandIndex = 0;
            CommandCount = 0;
        }
    }
}