using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class zztmn : HApp,Hservice
    {
        List<string> al = new List<string>();
        List<long> qq = new List<long>();
        long superUser = 992951869;
        public zztmn()
        {
            isblocked = true;
            iswhite = true;
            li.Add(614123891);
            li.Add(776324219);
            li.Add(341475083);
            li.Add(397777390);
            li.Add(274774820);
        }
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            if (!isopened(e.FromGroup.Id)) return;
            if (e.Message.Text.Length >= 2)
            {
                string ord = e.Message.Text.Substring(0, 2);
                if (ord == "/-")
                {
                    string ss = e.Message.Text.Substring(2).Trim();
                    if (al.Contains(ss))
                    {
                        int ind = al.IndexOf(ss);
                        //e.CQApi.SendGroupMessage(e.FromGroup, ind.ToString() + " " + al.Count.ToString() + " " + qq.Count.ToString());
                        if (qq[ind] == e.FromQQ.Id || e.FromQQ.Id == superUser)
                        {
                            al.Remove(ss);
                            qq.RemoveAt(ind);
                            e.CQApi.SendGroupMessage(e.FromGroup, "删除\"" + ss + "\"成功");
                        }
                        else
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, "Permission denied. 删除失败!");
                        }
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "未找到\"" + ss + "\"");
                    }

                }
                else if (ord == "/*")
                {
                    string info = "";
                    for (int i = 0; i < al.Count(); i++)
                    {
                        info += i.ToString() + " ";
                        info += al[i] + " ";
                        info += qq[i];
                        if (i != al.Count() - 1)
                        {
                            info += "\n";
                        }
                    }
                    e.CQApi.SendGroupMessage(e.FromGroup, info);
                }

            }
            //e.CQApi.SendGroupMessage(e.FromGroup, "???");
            if (e.Message.Text.Contains("我是") && !e.Message.Text.Contains("我是谁"))
            {
                //e.CQApi.SendGroupMessage(e.FromGroup, "???2");
                if (!al.Contains(e.Message.Text.Replace("我是", "").Trim()))
                {
                    al.Add(e.Message.Text.Replace("我是", "").Trim());
                    qq.Add(e.FromQQ.Id);
                }
            }
            else if (e.Message.Text.Contains("谁是"))
            {
                //e.CQApi.SendGroupMessage(e.FromGroup, "???3");
                if (!al.Contains(e.Message.Text.Replace("谁是", "").Trim()))
                {
                    al.Add(e.Message.Text.Replace("谁是", "").Trim());
                    qq.Add(e.FromQQ.Id);
                }
                Random a = new Random();
                e.CQApi.SendGroupMessage(e.FromGroup, (string)al[a.Next() % al.Count] + e.Message.Text.Replace("谁", "").Replace("你", "果啊k你").Replace("我", "果啊k我").Replace("果啊k你", "我").Replace("果啊k我", "你").Trim());
            }
            else if (e.Message.Text.Contains("是谁"))
            {
                //e.CQApi.SendGroupMessage(e.FromGroup, "???4");
                if (!al.Contains(e.Message.Text.Replace("是谁", "").Trim()))
                {
                    al.Add(e.Message.Text.Replace("是谁", "").Trim());
                    qq.Add(e.FromQQ.Id);
                }
                Random a = new Random();
                e.CQApi.SendGroupMessage(e.FromGroup, e.Message.Text.Replace("谁", "").Replace("你", "果啊k你").Replace("我", "果啊k我").Replace("果啊k你", "我").Replace("果啊k我", "你").Trim() + (string)al[a.Next() % al.Count]);
            }
    }
    }
}
