using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel.Dtos
{
    public class ClassMasterDtos
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }
}
