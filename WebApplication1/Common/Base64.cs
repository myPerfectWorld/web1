using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Common
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Base64
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Base64Encode(string content)
        {
            byte[] inArray = System.Text.Encoding.UTF8.GetBytes(content);
            content = Convert.ToBase64String(inArray);
            return content;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] inArray = Convert.FromBase64String(content);
            content = System.Text.Encoding.UTF8.GetString(inArray);
            return content;
        }
    }
}
