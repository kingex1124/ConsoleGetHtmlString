using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGetHtmlString
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pg = new Program();
            var result = pg.GetHtmlString("https://www.twse.com.tw/zh/page/trading/exchange/TWT48U.html");
        }

        public string GetHtmlString(string urlAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return data;
            }
            else
                return "False";
        }
    }
}
