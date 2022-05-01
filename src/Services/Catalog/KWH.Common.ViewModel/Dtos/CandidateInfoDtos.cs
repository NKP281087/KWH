using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel.Dtos
{
    public class CandidateInfoDtos
    {  
        public string ClassRollNo { get; set; } = string.Empty;
        public string CandidateName { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string AlternateNo { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string ICardNumber { get; set; } = string.Empty;
        public string GRNumber { get; set; } = string.Empty;
        public string RFId { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string ImpageUrl { get; set; } = string.Empty;         
        public string CreatedBy { get; set; } = string.Empty; 
        public string ModifiedBy { get; set; } = string.Empty;
    }
}
