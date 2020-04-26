using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class KWR : HApp, Hservice
    {

        public static ArrayList ge, re;
        public KWR()
        {
            ge = new ArrayList();
            re = new ArrayList();
        }
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            string t = e.Message.Text.Replace(" ", "");
            if(e.Message.Text.Contains("果果"))
            {
                string[] bds = { "憨", "傻", "笨", "丑", "爬", "爪巴", "滚", "hb", "sb" ,"垃圾","辣鸡","tlb","菜鸡","憨批","二愣子"};
                string[] fds = { "不", "没有", "怎么", "个屁", "个p" };
                int f = 1;
                foreach (string s in fds)
                {
                    int index = 0;
                    while ((index = e.Message.Text.IndexOf(s, index)) != -1)
                    {
                        f *= -1;
                        index++;
                    }
                }
                int tt = 1;
                foreach(string s in bds)
                {
                    if (e.Message.Text.Contains(s))
                    {
                        tt = -1;
                        break;
                    }
                }
                if(f * tt == -1)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "fnmdp");
                    FDHolder.d[e.FromGroup.Id].la.Add(new Mess("fnmdp", inf.hostqq));
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "果果真可爱");
                    FDHolder.d[e.FromGroup.Id].la.Add(new Mess("果果真可爱",inf.hostqq));
                }
            }
            
            for (int i = 0; i < ge.Count; i++)
            {
                //holog(ge[i] + "-" + e.Message + "-" + re[i]);
                if (Regex.IsMatch(t, (string)ge[i], RegexOptions.IgnoreCase))
                {
                    if (i == 0 && e.FromGroup == 812596623) continue;
                    e.CQApi.SendGroupMessage(e.FromGroup, (string)re[i]);
                    FDHolder.d[e.FromGroup.Id].la.Add(new Mess((string)re[i],inf.hostqq));
                    break;
                }
            }
        }
    }
}
