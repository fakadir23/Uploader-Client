using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.CrimeInformation
{
    public class RecoveryEntryDto
    {
        public string recoveryType { get; set; }
        public int? lookupId { get; set; }
        public string recoveryItemName { get; set; }
        public string amount { get; set; }
    }
}
