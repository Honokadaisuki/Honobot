using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xyz.hbyzhonoka.honobot.Code
{
    class YQT : HApp, Hservice
    {
        List<long> qqlis = new List<long>(new long[]{ 992951869, 949548985, 1841128521, 1834395340, 929519002, 1067104441, 1134384717, 1349330507, 1930955585, 1643952809, 1787945430, 731705278, 970347250, 1765495358 });
        bool[] has = new bool[14];
        public YQT()
        {
            isblocked = true;
            iswhite = true;
            li.Add(652388246);
            //li.Add(341475083);
        }
        public void RecMsg(CQGroupMessageEventArgs e)
        {
            if (!isopened(e.FromGroup.Id)) return;
            string[] s = {"t", "T","yt","Yt","yT","YT","[CQ:emoji,id=10004]"};
            string[] s2 = { "已填", "无异常", "已","填","ok"};
            bool ok = false;
            foreach(var i in s)
            {
                if(e.Message.Text.Trim() == i.Trim())
                {
                    ok = true;
                    break;
                }
            }
            foreach (var i in s2)
            {
                if (e.Message.Text.Trim().Contains(i))
                {
                    ok = true;
                    break;
                }
            }
            if (ok == false) return;
            if (!qqlis.Contains(e.FromQQ.Id))
            {
                e.CQApi.SendGroupMessage(e.FromGroup, "报过了还报你🐎呢?\n舍友们有没有人愿意帮忙回个血,大牌AOC显示器,1080p,可90°旋转升降,23.8寸,就我寝室里桌子上摆的那个,去年9月829元买的,一个人从老综搬到宿舍的,1月份放假之后就放那没用过,无磕碰99新,舍友温情价549元,送hdmi线,现在预约开学就给您上门安装好[CQ:face,id=107]");
            }
            else
            {
                qqlis.Remove(e.FromQQ.Id);
                string ts = "";
                ts += "[CQ:at,qq="+e.FromQQ.Id+"] 汇报成功!目前汇报进度:" + (14-qqlis.Count()) + "/14\n未汇报列表:\n";
                foreach(var i in qqlis)
                {
                    ts += "[CQ:at,qq=" + i + "]\n";
                }
                ts += "舍友们有没有人愿意帮忙回个血,大牌AOC显示器,1080p,可90°旋转升降,23.8寸,就我寝室里桌子上摆的那个,去年9月829元买的,一个人从老综搬到宿舍的,1月份放假之后就放那没用过,无磕碰99新,舍友温情价549元,送hdmi线,现在预约开学就给您上门安装好[CQ:face,id=107]";
                e.CQApi.SendGroupMessage(e.FromGroup, ts);
            }
        }
    }
}
