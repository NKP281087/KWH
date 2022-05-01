using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel.Dtos
{
    public class SectionDtos
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
