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
        Dictionary<long, List<string> > la;
        List<string> al;
        List<string> qq;
        string superUser = "992951869";
        long wz = -1;
        const int bc = 11;
        int step = 0;
        int[,] qp;
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
            la = new Dictionary<long, List<string> >();
            al.Add("谷梦龙");
            qq.Add(superUser);
        }
        void holog(string s)
        {
            WriteMessage("log.txt", s + "\n");
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
        public string printqp()
        {
            string s = "";
            s += "🎅";
            for(int i = 0;i < bc;i++)
            {
                if (i % 2 == 0)
                {
                    s += "🍎";
                }
                else
                {
                    s += "🍋";
                }
            }
            s += "🎅";
            s += "\n";
            for (int i = 0;i < bc;i++)
            {
                if(i % 2 == 0)
                {
                    s += "🍎";
                }
                else
                {
                    s += "🍋";
                }
                for(int j = 0;j < bc;j++)
                {
                    if(qp[i,j] == 1)
                    {
                        s += "👴";
                    }else if(qp[i,j] == 0)
                    {
                        s += "🐴";
                    }
                    else
                    {
                        s += "❤️";
                    }
                }
                if (i % 2 == 0)
                {
                    s += "🍎";
                }
                else
                {
                    s += "🍋";
                }
                s += "\n";
            }
            s += "🎅";
            for (int i = 0; i < bc; i++)
            {
                if (i % 2 == 0)
                {
                    s += "🍎";
                }
                else
                {
                    s += "🍋";
                }
            }
            s += "🎅";
            if (step != -1)
            {
                s += "\n";
                s += "现在轮到:";
                if (step % 2 == 0)
                {
                    s += "👴";
                }
                else
                {
                    s += "🐴";
                }
            }
            return s;
        }
        public bool pdw()
        {
            for(int i = 0;i < bc;i++)
            {
                for(int j = 0;j < bc;j++)
                {
                    if (qp[i, j] == 2) continue;
                    int k;
                    for (k = 1; i + k < bc && j + k < bc && qp[i + k, j + k] == qp[i, j]; k++) ;
                    if (k == 5) return true;
                    for (k = 1; i + k < bc && qp[i + k, j] == qp[i, j]; k++) ;
                    if (k == 5) return true;
                    for (k = 1; j + k < bc && qp[i, j + k] == qp[i, j]; k++) ;
                    if (k == 5) return true;
                    for (k = 1; i - k < bc && j + k < bc && i - k > 0 && qp[i - k, j + k] == qp[i, j]; k++) ;
                    if (k == 5) return true;
                }
            }
            return false;
        }
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            if (!ok)
            {
                ok = true;
                init();
            }
            //五子棋
            if (e.Message.Length > 3)
            {
                if (e.Message.Substring(0, 3) == "/wz")
                {
                    //Common.CqApi.SendGroupMessage(e.FromGroup, "?");
                    if (e.Message == "/wz start")
                    {
                        if (e.FromGroup == wz)
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "五子棋已经开始");
                        }
                        else if (wz == -1)
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "五子棋开始");
                            step = 0;
                            wz = e.FromGroup;
                            qp = new int[bc, bc];
                            for (int i = 0; i < bc; i++)
                            {
                                for (int j = 0; j < bc; j++)
                                {
                                    qp[i, j] = 2;
                                }
                            }
                            Common.CqApi.SendGroupMessage(e.FromGroup, printqp());
                            step++;
                        }

                    }
                    else if (e.Message == "/wz reset")
                    {
                        if (wz == e.FromGroup)
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "五子棋开始");
                            step = 0;
                            wz = e.FromGroup;
                            qp = new int[bc, bc];
                            for (int i = 0; i < bc; i++)
                            {
                                for (int j = 0; j < bc; j++)
                                {
                                    qp[i, j] = 2;
                                }
                            }
                            Common.CqApi.SendGroupMessage(e.FromGroup, printqp());
                            step++;
                        }
                    }
                    else
                    {
                        if (e.FromGroup == wz)
                        {
                            try
                            {
                                string[] s = e.Message.Split(' ');
                                int x = Convert.ToInt32(s[1]);
                                int y = Convert.ToInt32(s[2]);
                                if (qp[x - 1, y - 1] != 2)
                                {
                                    Common.CqApi.SendGroupMessage(e.FromGroup, "已经有棋了!");
                                }
                                else
                                {
                                    qp[x - 1, y - 1] = step % 2;
                                    if (pdw())
                                    {
                                        if (step % 2 == 0)
                                        {
                                            Common.CqApi.SendGroupMessage(e.FromGroup, "🐴" + "赢了!");
                                        }
                                        else
                                        {
                                            Common.CqApi.SendGroupMessage(e.FromGroup, "👴" + "赢了!");
                                        }
                                        wz = -1;
                                        step = -1;
                                    }
                                    Common.CqApi.SendGroupMessage(e.FromGroup, printqp());
                                    step++;
                                }
                            }
                            catch
                            {
                                Common.CqApi.SendGroupMessage(e.FromGroup, "命令解释失败");
                            }
                        }
                    }
                }
            }

            //复读
            string t = e.Message.Replace(" ", "");
            if(!la.ContainsKey(e.FromGroup))
            {
                la[e.FromGroup] = new List<string>();
            }
            la[e.FromGroup].Add(t);
            if(la[e.FromGroup].Count > 200)
            {
                la[e.FromGroup].RemoveAt(0);
            }
            for (int i = la[e.FromGroup].Count-1;2 * i - la[e.FromGroup].Count >= 0;i--)
            {
                bool ok = true;
                for(int j = i;j < la[e.FromGroup].Count;j++)
                {
                    if(la[e.FromGroup][i + j - la[e.FromGroup].Count()] != la[e.FromGroup][j])
                    {
                        ok = false;
                        break;
                    }
                }
                if(ok)
                {
                    if(!la[e.FromGroup][la[e.FromGroup].Count-1].Contains("复读警察出警"))
                    {
                        Common.CqApi.SendGroupMessage(e.FromGroup, "复读警察出警👮");
                    }
                    else
                    {
                        Common.CqApi.SendGroupMessage(e.FromGroup, "复读你🐎呢?");
                    }
                    la[e.FromGroup].Clear();
                    break;
                }
            }

            //正则匹配
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

            //zzt模拟器
            if(e.FromGroup == 614123891 || e.FromGroup == 776324219 || e.FromGroup == 341475083 || e.FromGroup == 397777390 || e.FromGroup == 274774820)
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
                                info += "\n";
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
