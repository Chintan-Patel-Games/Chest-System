using System.Collections.Generic;

namespace ChestSystem.Command
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();

        public void RegisterCommand(ICommand commandToRegister) => commandRegistry.Push(commandToRegister);

        public void Undo()
        {
            if (!RegistryEmpty())
                commandRegistry.Pop().Undo();
        }

        private bool RegistryEmpty() => commandRegistry.Count == 0;
    }
}