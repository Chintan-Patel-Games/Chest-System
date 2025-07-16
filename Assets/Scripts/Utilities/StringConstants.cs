using System;

namespace ChestSystem.Utilities
{
    public static class StringConstants
    {
        public const string ChestLocked = "Locked.";
        public const string ChestUnlocking = "Unlocking in ";
        public const string ChestUnlocked = "Unlocked.";
        public const string ChestOpened = "Opened.";

        public const string ZeroTimer = "00:00:00";

        public static string GetFormattedRemainingTime(TimeSpan remaining) =>
            string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);   
    }
}