using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace xyz.hbyzhonoka.honobot.Code
{
    class ycm : HApp, Hservice
    {

        private string GetData(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "GET";                      //确定GET模式
            myRequest.ContentType = "application/json";

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return content;

        }
        
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            string s = e.Message.Text;
            if(s == "ycm")
            {
                string c = GetData("https://api.bandoristation.com/?function=query_room_number");
                RootObject rb = JsonConvert.DeserializeObject<RootObject>(c);
                if(rb.status == "success")
                {
                    if(rb.response.Count() == 0)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "myc");
                    }
                    else
                    {
                        string sendt = "";
                        long nowt = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                        foreach (var res in rb.response)
                        {
                            sendt += res.number + "(" + Regex.Unescape(res.raw_message) + ") " + (decimal)(nowt - Convert.ToInt64(res.time))/1000 +"秒前\n";
                        }
                        e.CQApi.SendGroupMessage(e.FromGroup, sendt);
                    }
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "Error!");
                }
                
            }
        }
    }
    public class Source_info
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class User_info
    {
        public string type { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
    }

    public class Response
    {
        public string number { get; set; }
        public string raw_message { get; set; }
        public Source_info source_info { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public User_info user_info { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public List<Response> response { get; set; }
    }
}
