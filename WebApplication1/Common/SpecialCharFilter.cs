using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SpecialCharFilter
    {
        /// <summary>
        /// 获取微信昵称时，emoj图标无法转存到数据库
        /// </summary>
        /// <param name="emojiString"></param>
        /// <returns></returns>
        public static string EmojiFilter(string emojiString)
        {
            foreach (var a in emojiString)
            {
                byte[] bts = Encoding.UTF32.GetBytes(a.ToString());

                if (bts[0].ToString() == "253" && bts[1].ToString() == "255")
                {
                    emojiString = emojiString.Replace(a.ToString(), "");
                }

            }
            return emojiString;
        }
    }
}
