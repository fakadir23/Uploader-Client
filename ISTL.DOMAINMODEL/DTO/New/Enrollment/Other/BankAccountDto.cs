using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class BankAccountDto
    {
        public string accountNo { get; set; }
        public string atmCardNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string creditCardNo { get; set; }
        public string typeOfAccount { get; set; }
    }
}
