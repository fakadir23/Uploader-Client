using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class ForeignAddressDto
    {
        public int? country { get; set; }
        public string houseOrAptNo { get; set; }
        public string socialSecurityNo { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string zipOrPostCode { get; set; }
    }
}
