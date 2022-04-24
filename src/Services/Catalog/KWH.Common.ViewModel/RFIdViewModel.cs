using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel
{
    public class RFIdViewModel
    {
        public int RFIdNo { get; set; } = 0;
        public string TimeIn { get; set; } = string.Empty;
        public string TimeOut { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
