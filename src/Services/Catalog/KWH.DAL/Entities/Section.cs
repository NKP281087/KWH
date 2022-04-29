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
        [Required]
        public Guid SectionId { get; set; }    

        [Required (ErrorMessage ="Please Enter Section Name")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string SectionName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool OnHold { get; set; } = false ;
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

    }
}
