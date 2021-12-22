using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace BlizzardFetcher.Helpers
{
    public class GenericHelper
    {
        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }


        public long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }
        

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentUnixTimestampMillis()
        {
            DateTime localDateTime, univDateTime;
            localDateTime = DateTime.Now;
            univDateTime = localDateTime.ToUniversalTime();
            return (long)(univDateTime - UnixEpoch).TotalMilliseconds;
        }
        public String lettercaseingFormating(String str)
        {
            char[] ch = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0 && ch[i] != ' ' ||
                    ch[i] != ' ' && ch[i - 1] == ' ')
                {
                    if ( ch[i] >= 'a' && ch[i] <= 'z')
                    {    
                         ch[i] = (char)(ch[i] - 'a' + 'A');
                    }
                }
                else if (ch[i] >= 'A' && ch[i] <= 'Z')
                    ch[i] = (char)(ch[i] + 'a' - 'A');
            }
            String st = new String(ch);
            return st;
        }

        
        public string HTTPGET(string url)
        {
            var client = new TimeoutWebClient(new TimeSpan(0,1,0));
            //Console.WriteLine("Http request: " + url);

            var response = client.DownloadString(url);
            return response;
        }

        public string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str.Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-");
        }

        public class TimeoutWebClient : WebClient
        {
            public TimeSpan Timeout { get; set; }

            public TimeoutWebClient(TimeSpan timeout)
            {
                Timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri uri)
            {
                var request = base.GetWebRequest(uri);
                if (request == null)
                {
                    return null;
                }

                var timeoutInMilliseconds = (int)Timeout.TotalMilliseconds;

                request.Timeout = timeoutInMilliseconds;
                if (request is HttpWebRequest httpWebRequest)
                {
                    httpWebRequest.ReadWriteTimeout = timeoutInMilliseconds;
                }

                return request;
            }
        }

        public Auth Get_Access_TokenAsync(string clientID, string clientSecret)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://us.battle.net/oauth/token"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($@"{clientID}:{clientSecret}"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = JsonConvert.DeserializeObject<Auth>(httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result);
                    return response;


                }
            }
        }

        public string RemoveDoubles(string text,string item)
        {
            var newText = text.Replace(item + item, item);
            if (text.Contains(item + item))
            {
                return RemoveDoubles(newText, item);
            }
            return newText;
        }
    }
}
