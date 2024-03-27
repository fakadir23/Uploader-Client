using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary
{
    public class BatchRegisterBeneficiaryRequest
    {
        public List<RegisterBeneficiaryRequest> beneficiaries { get; set; }

        public BatchRegisterBeneficiaryRequest()
        {
            beneficiaries = new List<RegisterBeneficiaryRequest>();
        }
    }
}
