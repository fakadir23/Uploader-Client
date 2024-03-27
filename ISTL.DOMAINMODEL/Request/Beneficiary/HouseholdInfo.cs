using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.Beneficiary
{
    public class HouseholdInfo
    {
        public string applicationId { get; set; }
        public int? femaleBoth { get; set; }
        public int? femaleChronicalIll { get; set; }
        public int? femaleDisable { get; set; }
        public int? femaleTotal { get; set; }
        public int? maleBoth { get; set; }
        public int? maleChronicalIll { get; set; }
        public int? maleDisable { get; set; }
        public int? maleTotal { get; set; }
    }
}
