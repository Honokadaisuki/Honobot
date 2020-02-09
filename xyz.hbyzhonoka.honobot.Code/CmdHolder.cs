using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    public static class CmdHolder
    {
        public static Dictionary<string, Cmdable> ma = new Dictionary<string, Cmdable>();
        public static void CmdAnalysis(CQGroupMessageEventArgs e)
        {
            string s = e.Message.Text.Substring(1).Split(' ')[0];
            if(ma.ContainsKey(s))
            {
                ma[s].Revcom(e.Message.Text.Split(' '));
            }
        }
    }
}
