using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class HApp
    {
        public bool isblocked = false;
        public bool iswhite = true;
        public List<long> li = new List<long>();
        public bool isopened(long id)
        {
            bool con = li.Contains(id);
            return (iswhite && con) || ((!iswhite) && (!con));
        }
    }
}
