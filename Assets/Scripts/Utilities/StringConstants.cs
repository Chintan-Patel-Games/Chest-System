using System;

namespace ChestSystem.Utilities
{
    public static class StringConstants
    {
        public const string ChestLocked = "Locked.";
        public const string ChestUnlocking = "Unlocking in ";
        public const string ChestUnlocked = "Unlocked.";
        public const string ChestOpened = "Opened.";

        public const string OpenAnimParam = "Open";

        public const string ZeroTimer = "00:00:00";

        public const string UnlockChestWarning = "Can only unlock one chest at a time!";
        public const string NotEnoughGemsWarning = "Not enough gems to unlock this chest!";

        public static string GetFormattedRemainingTimeMinutes(TimeSpan remaining) =>
            string.Format("{0:D2}:00", remaining.Minutes);
        public static string GetFormattedRemainingTimeHours(TimeSpan remaining) =>
            string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);

        public static string FormatRemainingTime(TimeSpan remaining)
        {
            if (remaining.TotalMinutes <= 30)
                return $"{remaining.Minutes:D2}:{remaining.Seconds:D2}"; // MM:SS
            else
                return $"{(int)remaining.TotalHours:D2}:{remaining.Minutes:D2}:{remaining.Seconds:D2}"; // HH:MM:SS
        }
    }
}