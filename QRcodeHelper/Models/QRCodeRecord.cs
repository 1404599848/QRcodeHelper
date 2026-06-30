using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper.Models
{
    public class QRCodeRecord
    {
        public long Id { get; set; } 

        public string QRCode { get; set; }

        public string Level { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public int AlertType { get; set; } = 0;

        public string AlertTypeDisplay
        {
            get
            {
                switch (AlertType)
                {
                    case 1: return "重码";
                    case 2: return "漏码";
                    case 3: return "跳号";
                    default: return "正常";
                }
            }
        }

        public IsPassed IsPassed 
        { 
            get
            {
                if (Level == "A" || Level == "B")
                    return IsPassed.通过;
                else
                    return IsPassed.未通过;
            }
        }
    }
}
