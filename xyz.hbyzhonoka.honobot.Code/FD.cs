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
        List<string> la = new List<string>();
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
                bool ok = true;
                for (int j = i; j < la.Count; j++)
                {
                    if (la[i + j - la.Count()] != la[j])
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    if (!la[la.Count - 1].Contains("复读警察出警"))
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "复读警察出警👮");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, "复读你🐎呢?");
                    }
                    la.Clear();
                    break;
                }
            }
        }
    }
}
