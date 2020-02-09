using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    public static class HServicesHolder
    {
        public static List<HHolder> l = new List<HHolder>();
        public static void MsgAnalysis(CQGroupMessageEventArgs e)
        {
            foreach(var i in l)
            {
                i.RecMsg(e);
            }
        }
    }
}
