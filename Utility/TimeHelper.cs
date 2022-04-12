using System;

namespace MMORPG_AccountServer
{
    public class TimeHelper
    {
        /// <summary>
        /// 当前时间戳，单位ms
        /// </summary>
        public static long NowTimestamp
        {
            get
            {
                //Ticks：计时周期数，是指一个时区的1年1月1日0时0分0秒，到之后该时区的某个时间，这段时间间隔的100纳秒个数，1Ticks=100ns
                //DateTime.UtcNow.Ticks是0时区的当前时间的计时周期数，new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks是0时区的1970年1月1日0时0分0秒的计时周期数，即常量621355968000000000
                //所以0时区的1970年1月1日0时0分0秒到当前时间的毫秒数为(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks) * 100 / 1000000，化简可得
                return (DateTime.UtcNow.Ticks - 621355968000000000) / 10000;
            }
        }
    }
}