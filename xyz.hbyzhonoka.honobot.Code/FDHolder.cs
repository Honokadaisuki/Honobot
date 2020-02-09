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
        Dictionary<long, FD> d = new Dictionary<long, FD>();
        // override object.Equals
        public FDHolder()
        {
            isblocked = true;
            iswhite = true;
            li.Add(341475083);
            li.Add(614123891);
        }
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            if (!isopened(e.FromGroup.Id)) return;
            if(!d.ContainsKey(e.FromGroup.Id))
            {
                d[e.FromGroup.Id] = new FD();
                e.CQLog.Debug("new", e.FromGroup.Id);
            }
            d[e.FromGroup.Id].RecMsg(e);
        }
    }
}
