using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Constants
{
    public static class RedisKeys
    {
        public const string SystemName = "GameStats";

        public const string Users = SystemName + ":Users.Hash";
        public const string SequentialId = SystemName + "UserIds.String";

        public const string StartupCache = SystemName + ":StartupCache.List";

        public const string ActiveCache = SystemName + ":ActiveCache.List";


        private const string DailyNewUserCount = SystemName + ":DailyNewUserCount";

        private const string HourlyNewUserCount = SystemName + ":HourlyNewUserCount";

        private const string DailyActiveUserCount = SystemName + ":DailyActiveUserCount";

        private const string DailyLoginUserCount = SystemName + ":DailyLoginCount";

        private const string HourlyLoginUserCount = SystemName + ":HourlyLoginCount";


        public static string GetDailyNewUserCountKey(DateTimeOffset? dateTime) => $"{DailyNewUserCount}:{(dateTime ?? DateTimeOffset.Now).ToString("yyyyMMdd")}";

        public static string GetDailyActiveUserCountKey(DateTimeOffset? dateTime) => $"{DailyActiveUserCount}:{(dateTime ?? DateTimeOffset.Now).ToString("yyyyMMdd")}";

        public static string GetDailyLoginUserCountKey(DateTimeOffset? dateTime) => $"{DailyLoginUserCount}:{(dateTime ?? DateTimeOffset.Now).ToString("yyyyMMdd")}";

        public static string GetHourlyNewUserCountKey(DateTimeOffset? dateTime) => $"{HourlyNewUserCount}:{(dateTime ?? DateTimeOffset.Now).ToString("yyyyMMddHH")}";

        public static string GetHourlyLoginUserCountKey(DateTimeOffset? dateTime) => $"{HourlyLoginUserCount}:{(dateTime ?? DateTimeOffset.Now).ToString("yyyyMMddHH")}";
    }
}
