using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Main;

namespace ChestSystem.Command
{
    public class UnlockWithGemsCommand : ICommand
    {
        public CommandData commandData;

        public UnlockWithGemsCommand(CommandData commandData) => this.commandData = commandData;

        public void Undo()
        {
            if (commandData.ChestSlot.GetCurrentState() is UnlockedChestState)
            {
                commandData.ChestSlot.SetState(new LockedChestState(commandData.ChestSlot));
                commandData.ChestSlot.SlotLockedState();
                commandData.ChestSlot.ResetTimer();
                GameService.Instance.CurrencyService.AddGems(commandData.GemsSpent);
                GameService.Instance.EventService.OnUndo.InvokeEvent(commandData.ChestSlot);
            }
        }
    }
}