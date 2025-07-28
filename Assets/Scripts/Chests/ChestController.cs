namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestSO chestData;
        private ChestView chestView;

        public ChestController(ChestSO chestData, ChestView chestView)
        {
            this.chestData = chestData;
            this.chestView = chestView;

            chestView.Initialize(chestData);
            chestView.PlayLockAnimation();
        }

        public void CollectChest()
        {
            chestView.PlayOpenAnimation();
            // Grant rewards, etc.
        }

        public ChestSO GetChestData() => chestData;

        public ChestView GetChestView() => chestView;

        ~ChestController() => chestView?.PlayLockAnimation();
    }
}