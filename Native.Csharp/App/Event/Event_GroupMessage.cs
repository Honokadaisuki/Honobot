using Native.Csharp.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.EventArgs;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using Tesseract;
using System.Drawing;

namespace Native.Csharp.App.Event
{
    class Event_GroupMessage : IReceiveGroupMessage
    {
        ArrayList ge,re;
        bool ok = false;
        Dictionary<long, string[]> la;
        List<string> al;
        List<string> qq;
        string superUser = "992951869";
        public void WriteMessage(string path, string msg)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.Write(msg);
                    sw.Flush();
                }
            }
        }
        public void ReadData()
        {

            StreamReader sr = new StreamReader("rules.honoka", Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                ge.Add(line.Split((char)3)[0]);
                re.Add(line.Split((char)3)[1]);
            }
        }
        void init()
        {
            ge = new ArrayList();
            re = new ArrayList();
            ReadData();
            al = new List<string>();
            qq = new List<string>();
            la = new Dictionary<long, string[]>();
            al.Add("谷梦龙");
            qq.Add(superUser);
        }
        void holog(string s)
        {
            WriteMessage("log.txt", s + "\r\n");
        }
        private string Gettxt(string imagePath)
        {
            string text = "";
            List<string> resultList = new List<string>();
            using (var ocr = new TesseractEngine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"), "eng", EngineMode.Default))
            {
                var pix = PixConverter.ToPix(new Bitmap(imagePath));
                using (var page = ocr.Process(pix))
                {
                    text = page.GetText();
                }
            }
            return text;
        }
        
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {

            //if (e.FromGroup != 341475083) return;
            string t = e.Message.Replace(" ", "");
            //if (e.FromGroup == 812596623) return;
            if (!ok)
            {
                ok = true;
                init();
            }
            if(la.ContainsKey(e.FromGroup))
            {
                if (t == la[e.FromGroup][1] && la[e.FromGroup][0] != la[e.FromGroup][1])
                {
                    if (t.Contains("复读警察出警"))
                    {
                        Common.CqApi.SendGroupMessage(e.FromGroup, "?");
                    }
                    else
                    {
                        Common.CqApi.SendGroupMessage(e.FromGroup, "复读警察出警👮");
                    }
                    la[e.FromGroup][0] = la[e.FromGroup][1];
                    la[e.FromGroup][1] = t + "???";
                }
                else
                {
                    la[e.FromGroup][0] = la[e.FromGroup][1];
                    la[e.FromGroup][1] = t;
                }
            }
            else
            {
                la.Add(e.FromGroup, new string[2] { "", t });
            }

            for (int i = 0; i < ge.Count; i++)
            {
                //holog(ge[i] + "-" + e.Message + "-" + re[i]);
                if (Regex.IsMatch(t, (string)ge[i], RegexOptions.IgnoreCase))
                {
                    if (i == 0 && e.FromGroup == 812596623) continue;
                    Common.CqApi.SendGroupMessage(e.FromGroup, (string)re[i]);
                    break;
                }
            }
            /*if(Regex.IsMatch(e.Message, "CQ:image:.*"))
            {
                MatchCollection mc = Regex.Matches(e.Message, "CQ:image:.*?");
                foreach(Match m in mc)
                {
                    holog(m.ToString());
                }
            }*/
            if(e.FromGroup == 614123891 || e.FromGroup == 776324219 || e.FromGroup == 341475083 || e.FromGroup == 397777390)
            {
                if (e.Message.Length >= 2)
                {
                    string ord = e.Message.Substring(0, 2);
                    if (ord == "/-")
                    {
                        string ss = e.Message.Substring(2).Trim();
                        if (al.Contains(ss))
                        {
                            int ind = al.IndexOf(ss);
                            //Common.CqApi.SendGroupMessage(e.FromGroup, ind.ToString() + " " + al.Count.ToString() + " " + qq.Count.ToString());
                            holog(ind.ToString() + " " + al.Count.ToString() + " " + qq.Count.ToString());
                            if (qq[ind] == e.FromQQ.ToString() || e.FromQQ.ToString() == superUser)
                            {
                                holog("2");
                                al.Remove(ss);
                                qq.RemoveAt(ind);
                                Common.CqApi.SendGroupMessage(e.FromGroup, "删除\"" + ss + "\"成功");
                            }
                            else
                            {
                                Common.CqApi.SendGroupMessage(e.FromGroup, "Permission denied. 删除失败!");
                            }
                        }
                        else
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "未找到\"" + ss + "\"");
                        }

                    }else if(ord == "/*")
                    {
                        string info = "";
                        for(int i = 0;i < al.Count();i++)
                        {
                            info += i.ToString() + " ";
                            info += al[i] + " ";
                            info += qq[i];
                            if(i != al.Count()-1)
                            {
                                info += "\r\n";
                            }
                        }
                        Common.CqApi.SendGroupMessage(e.FromGroup, info);
                    }
                }
                //Common.CqApi.SendGroupMessage(e.FromGroup, "???");
                if (e.Message.Contains("我是") && !e.Message.Contains("我是谁"))
                {
                    //Common.CqApi.SendGroupMessage(e.FromGroup, "???2");
                    if (!al.Contains(e.Message.Replace("我是", "").Trim()))
                    {
                        al.Add(e.Message.Replace("我是", "").Trim());
                        qq.Add(e.FromQQ.ToString());
                    }
                }else if(e.Message.Contains("谁是"))
                {
                    //Common.CqApi.SendGroupMessage(e.FromGroup, "???3");
                    if (!al.Contains(e.Message.Replace("谁是", "").Trim()))
                    {
                        al.Add(e.Message.Replace("谁是", "").Trim());
                        qq.Add(e.FromQQ.ToString());
                    }
                    Random a = new Random();
                    Common.CqApi.SendGroupMessage(e.FromGroup, (string)al[a.Next() % al.Count] + e.Message.Replace("谁","").Replace("你","果啊k你").Replace("我", "果啊k我").Replace("果啊k你","我").Replace("果啊k我", "你").Trim());
                }
                else if (e.Message.Contains("是谁"))
                {
                    //Common.CqApi.SendGroupMessage(e.FromGroup, "???4");
                    if (!al.Contains(e.Message.Replace("是谁", "").Trim()))
                    {
                        al.Add(e.Message.Replace("是谁", "").Trim());
                        qq.Add(e.FromQQ.ToString());
                    }
                    Random a = new Random();
                    Common.CqApi.SendGroupMessage(e.FromGroup, e.Message.Replace("谁", "").Replace("你", "果啊k你").Replace("我", "果啊k我").Replace("果啊k你", "我").Replace("果啊k我", "你").Trim() + (string)al[a.Next() % al.Count]);
                }
            }
            
        }
    }
}
