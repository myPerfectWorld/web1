using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpUtilty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> pairs)
        {
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "application/json, text/javascript, */*"; //"text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";

                byte[] buffer = encoding.GetBytes(fomateParams(pairs));
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                sb.Append(sr.ReadToEnd());
                //string ssb = sb.ToString();
                throw new Exception(sb.ToString());
            }
        }
        /*
         *  url:POST请求地址
         *  postData:json格式的请求报文,例如：{"key1":"value1","key2":"value2"}
         */
         /// <summary>
         /// 
         /// </summary>
         /// <param name="url"></param>
         /// <param name="postData"></param>
         /// <returns></returns>
        public static string PostUrl(string url, string postData)
        {
            string result = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";

            req.Timeout = 90000;//设置请求超时时间，单位为毫秒

            req.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(postData);

            req.ContentLength = data.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);

                reqStream.Close();
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            Stream stream = resp.GetResponseStream();

            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        /// <summary>  
        /// GET Method  
        /// </summary>  
        /// <returns></returns>  
        public static string HttpGet(string url, Dictionary<string, string> pairs)
        {
            if (pairs.Count() > 0)
            {
                url = url + "?" + fomateParams(pairs);
            }
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                return content;
            }
            //异常请求  
            catch (WebException e)
            {
                myResponse = (HttpWebResponse)e.Response;
                using (Stream errData = myResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        string text = reader.ReadToEnd();

                        return text;
                    }
                }
            }
        }
        /// <summary>  
        /// GET Method  
        /// </summary>  
        /// <returns></returns>  
        public static string HttpGet(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                reader.Close();
                return content;
            }
            //异常请求  
            catch (WebException e)
            {
                myResponse = (HttpWebResponse)e.Response;
                using (Stream errData = myResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(errData))
                    {
                        string text = reader.ReadToEnd();

                        return text;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string fomateParams(Dictionary<string, string> pairs)
        {
            string body = string.Empty;
            if (pairs.Count > 0)
            {
                var pararr = pairs.Select(a => a.Key + "=" + a.Value).ToArray();
                body = string.Join("&", pararr);
            }
            return body;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static async Task<string> PostMobelGameMessageUrl(string url, string postData)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";

            req.Timeout = 9000;//设置请求超时时间，单位为毫秒

            req.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(postData);

            req.ContentLength = data.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);

                reqStream.Close();
            }
            await req.GetResponseAsync();
            //HttpWebResponse resp =(HttpWebResponse)req.GetResponseAsync();

            //Stream stream = resp.GetResponseStream();

            ////获取响应内容
            //using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //{
            //    result = reader.ReadToEnd();
            //}
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pairs"></param>
        /// <param name="manageToken"></param>
        /// <returns></returns>
        public static string HttpPostTest(string url, Dictionary<string, string> pairs, string manageToken)
        {
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "application/json, text/javascript, */*"; //"text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] buffer = encoding.GetBytes(fomateParams(pairs));
                request.Headers.Add("Authorization", "QBox " + manageToken);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;
                StringBuilder sb = new StringBuilder();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                sb.Append(sr.ReadToEnd());
                //string ssb = sb.ToString();
                throw new Exception(sb.ToString());
            }
        }
    }
}
