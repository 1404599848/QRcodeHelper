using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper
{
    public enum IsPassed
    {
        通过 = 0,
        未通过 = 1
    }

    public enum AlertType
    {
        正常 = 0,
        重码 = 1,
        漏码 = 2
    }
}
