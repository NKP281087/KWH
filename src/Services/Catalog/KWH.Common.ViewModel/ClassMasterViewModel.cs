using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel
{
    public class ClassMasterViewModel
    {
        public Guid ClassId { get; set; }
        public Guid SectionId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string SectionName { get; set; } = string.Empty;
    }
}
