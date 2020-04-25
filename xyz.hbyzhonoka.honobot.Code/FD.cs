using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class FD : Hservice
    {
        public List<string> la = new List<string>();
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            string t = e.Message.Text.Replace(" ", "");
            la.Add(t);
            if (la.Count > 200)
            {
                la.RemoveAt(0);
            }
            for (int i = la.Count - 1; 2 * i - la.Count >= 0; i--)
            {
                int tip1 = 2 * i - la.Count();
                bool ok = true;
                for (int j = 0; j <= la.Count()-1-i; j++)
                {
                    if (la[tip1 + j] != la[i + j])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    /*if (la[la.Count - 1] != "[CQ: emoji, id = 128110]复[CQ: emoji, id = 128110]读[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]察[CQ: emoji, id = 128110]出[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]".Replace(" ", ""))
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "👮复👮读👮警👮察👮出👮警👮");
                        la.Add("[CQ: emoji, id = 128110]复[CQ: emoji, id = 128110]读[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]察[CQ: emoji, id = 128110]出[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]".Replace(" ", ""));
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "复读你🐎呢?");
                        la.Add("复读你[CQ: emoji, id = 128014]呢 ? ".Replace(" ", ""));
                    }
                    break;*/
                    for (int j = 0; j <= la.Count() - 1 - i; j++)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, la[tip1 + j]);
                    }
                    la.Clear();
                    break;
                }
            }
        }
    }
}
