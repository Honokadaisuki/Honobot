using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class FDHolder : HApp,HHolder
    {
        public static Dictionary<long, FD> d = new Dictionary<long, FD>();
        public FDHolder()
        {
            isblocked = true;
            iswhite = true;
            li.Add(614123891);
            li.Add(776324219);
            li.Add(341475083);
            li.Add(397777390);
            li.Add(274774820);
            li.Add(216120896);
            li.Add(652388246);
            li.Add(707873872);
        }
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            if (!isopened(e.FromGroup.Id)) return;
            if (e.Message.Text.Replace(" ", "") == "/teach [CQ: emoji, id = 128110]复[CQ: emoji, id = 128110]读[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]察[CQ: emoji, id = 128110]出[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110] [CQ: emoji, id = 128110]复[CQ: emoji, id = 128110]读[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]察[CQ: emoji, id = 128110]出[CQ: emoji, id = 128110]警[CQ: emoji, id = 128110]".Replace(" ", ""))
            {
                e.CQApi.SendGroupMessage(e.FromGroup, "/delete [CQ:emoji,id=128110]复[CQ:emoji,id=128110]读[CQ:emoji,id=128110]警[CQ:emoji,id=128110]察[CQ:emoji,id=128110]出[CQ:emoji,id=128110]警[CQ:emoji,id=128110]");
                return;
            }
            if(!d.ContainsKey(e.FromGroup.Id))
            {
                d[e.FromGroup.Id] = new FD();
                e.CQLog.Debug("new", e.FromGroup.Id);
            }
            d[e.FromGroup.Id].RecMsg(e);
        }
    }
}
