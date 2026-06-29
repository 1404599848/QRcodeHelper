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
