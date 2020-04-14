using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public long getUtcTrick(DateTime time)
        {
            DateTimeOffset fs = new DateTimeOffset(time);
            long trick = (fs.UtcTicks - 621355968000000000) / 10000000;//秒值 
            return trick;
        }
        /// <summary>
        /// 十三位时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetMillisecondsUtcTrick()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}
