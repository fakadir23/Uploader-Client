using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Beneficiary
{
    public class BeneficiarySummaryDto
    {
        public int Serial { get; set; }
        public string ApplicationId { get; set; }
        public string BeneficiaryName { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string SelectionCriteria { get; set; }
    }
}
