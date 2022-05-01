using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.DAL.Entities
{
    [Table("Section")]
    public class Section
    {
        [Key]  
        public int SectionId { get; set; }      
        public string SectionName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool OnHold { get; set; } = false ;
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

    }
}
