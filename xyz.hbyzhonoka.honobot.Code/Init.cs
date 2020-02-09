using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    public class Init : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            HServicesHolder.l.Add(new FDHolder());
        }

        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            
        }
    }
}
