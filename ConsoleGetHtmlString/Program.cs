using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            var result = pg.GetHtmlString("http://128.110.22.51/e_leader/dayend/SPSRCD_BHNO_a_adm.asp");

            var result2 = pg.ReadHtmlPage("http://128.110.22.51/e_leader/dayend/SPSRCD_BHNO_a_adm.asp");
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

        public String ReadHtmlPage(string url)
        {

            //setup some variables

            String date = "20200723";

            //setup some variables end

            String result = "";
            String strPost = "TDATE=" + date;
            StreamWriter myWriter = null;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr =
               new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();

                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }

        public string ReadHtmlPage2(string url)
        {
            WebRequest req = WebRequest.Create(url);
            string postData = "TDATE=20200723";

            byte[] send = Encoding.Default.GetBytes(postData);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = send.Length;

            Stream sout = req.GetRequestStream();
            sout.Write(send, 0, send.Length);
            sout.Flush();
            sout.Close();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            string returnvalue = sr.ReadToEnd();

            return returnvalue;
        }
    }
}
