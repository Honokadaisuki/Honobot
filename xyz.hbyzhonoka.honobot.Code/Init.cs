using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    public class Init : IAppEnable
    {

        public void ReadData()
        {

            StreamReader sr = new StreamReader("rules.honoka", Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                KWR.ge.Add(line.Split((char)3)[0]);
                KWR.re.Add(line.Split((char)3)[1]);
                
            }
        }
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            HServicesHolder.l.Add(new FDHolder());
            HServicesHolder.l.Add(new zztmn());
            HServicesHolder.l.Add(new KWR());
            ReadData();
        }

        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            
        }
    }
}
