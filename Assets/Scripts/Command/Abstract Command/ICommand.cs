using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Command
{
    public interface ICommand
    {
        public void Undo();
    }

    public struct CommandData
    {
        public ChestSlotController ChestSlot;
        public int GemsSpent;

        public CommandData(ChestSlotController ChestSlot, int GemsSpent)
        {
            this.ChestSlot = ChestSlot;
            this.GemsSpent = GemsSpent;
        }
    }
}