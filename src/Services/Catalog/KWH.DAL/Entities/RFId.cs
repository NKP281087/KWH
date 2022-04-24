using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.DAL.Entities
{
    public class RFId
    {
        [Key]
        public int RFIdNo { get; set; } 

        [Required]
        public string TimeIn { get; set; }

        [Required]
        public string TimeOut { get; set; }

        public bool IsActive { get; set; } = true;
        public bool OnHold { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
