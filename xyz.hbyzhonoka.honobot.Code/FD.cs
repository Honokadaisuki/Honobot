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
        public List<Mess> la = new List<Mess>();
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            string t = e.Message.Text.Replace(" ", "");
            la.Add(new Mess(t,e.FromQQ.Id));
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
                    if (la[tip1 + j].m != la[i + j].m)
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
                    List<Mess> tl = new List<Mess>();
                    bool is_fdgg = true;
                    for (int j = 0; j <= la.Count() - 1 - i; j++)
                    {
                        if(la[tip1+j].q != inf.hostqq)
                        {
                            is_fdgg = false;
                        }
                    }
                    if(is_fdgg == false)
                    {
                        for (int j = 0; j <= la.Count() - 1 - i; j++)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, la[tip1 + j].m);
                            tl.Add(new Mess(la[tip1 + j].m, inf.hostqq));
                        }
                        la.Clear();
                        foreach (var gg in tl)
                        {
                            la.Add(gg);
                        }
                    }
                    else
                    {
                        if (e.CQApi.GetGroupMemberInfo(e.FromGroup.Id, inf.hostqq).MemberType == Native.Csharp.Sdk.Cqp.Enum.QQGroupMemberType.Creator ||
                       e.CQApi.GetGroupMemberInfo(e.FromGroup.Id, inf.hostqq).MemberType == Native.Csharp.Sdk.Cqp.Enum.QQGroupMemberType.Manage)
                        {
                            bool is_Set = false;
                            for (int j = 0; j <= la.Count() - 1 - i; j++)
                            {
                                if (e.CQApi.SetGroupMemberBanSpeak(e.FromGroup.Id, la[i + j].q, TimeSpan.FromMinutes(2)))
                                {
                                    is_Set = true;
                                }
                            }
                            if(is_Set)
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup.Id, "复读个b");
                                la.Add(new Mess("复读个b", inf.hostqq));
                            }
                        }
                    }
                    break;
                }
            }
        }

        private Mess Mess(string t, long id)
        {
            throw new NotImplementedException();
        }
    }
}
