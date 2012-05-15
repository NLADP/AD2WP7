using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AD2
{
    internal static class WebService
    {
        private static readonly CookieContainer _cookies = new CookieContainer();

        public delegate void GetCompleteHandler(bool success, string result);
        public delegate void PostCompleteHandler(bool success, string result);

        public static void Get(string url, GetCompleteHandler handler)
        {
            HttpWebRequest w = (HttpWebRequest) WebRequest.Create(Service.GetUri(url));
            w.CookieContainer = _cookies;
            w.Method = "GET";
            w.BeginGetResponse(delegate(IAsyncResult ar)
                                   {
                                       HttpWebResponse response = (HttpWebResponse) w.EndGetResponse(ar);
                                       if (response.StatusCode == HttpStatusCode.OK)
                                       {
                                           _cookies.Add(Service.BaseUri, response.Cookies);

                                           Stream sResponse = response.GetResponseStream();
                                           using (StreamReader sr = new StreamReader(sResponse))
                                           {
                                               string res = sr.ReadToEnd();
                                               handler(res != "Error" && res != "\"Login failed.\"", res);
                                           }
                                       }
                                   }, null);
        }

        public static void Post(string url, IDictionary<string, string> postData, PostCompleteHandler handler)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in postData)
            {
                sb.Append(p.Key).Append("=").Append(Uri.EscapeDataString(p.Value)).Append("&");
            }

            HttpWebRequest w = (HttpWebRequest) WebRequest.Create(Service.GetUri(url));
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            w.CookieContainer = _cookies;
            w.BeginGetRequestStream(delegate(IAsyncResult ar)
                                        {
                                            Stream stream = w.EndGetRequestStream(ar);
                                            using (StreamWriter sw = new StreamWriter(stream))
                                            {
                                                sw.Write(sb.ToString());
                                            }

                                            w.BeginGetResponse(delegate(IAsyncResult result)
                                                                   {
                                                                       // Jaah, briljant, geneste anonymous methods (yuck)
                                                                       HttpWebResponse response = (HttpWebResponse) w.EndGetResponse(result);
                                                                       _cookies.Add(Service.BaseUri, response.Cookies);
                                                                       if (response.StatusCode == HttpStatusCode.OK)
                                                                       {
                                                                           Stream sResponse =
                                                                               response.GetResponseStream();
                                                                           using (
                                                                               StreamReader sr =
                                                                                   new StreamReader(sResponse))
                                                                           {
                                                                               string res = sr.ReadToEnd();
                                                                               handler(res != "Error" && res != "\"Login failed.\"", res);
                                                                           }
                                                                       }
                                                                       else
                                                                       {
                                                                           handler(false, string.Empty);
                                                                       }
                                                                   }, null);
                                        }, null);
        }
    }
}
