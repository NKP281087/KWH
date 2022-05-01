using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel
{
    public class SectionViewModel
    {     
        public int SectionId { get; set; } 
        public string SectionName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true; 
    }
}
