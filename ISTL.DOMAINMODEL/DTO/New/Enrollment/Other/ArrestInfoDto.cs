using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment
{
    public class ArrestInfoDto
    {
        public string arrestedByAgency { get; set; }
        public string dateOfArrest { get; set; }
        public string disposal { get; set; }
        public int? district { get; set; }
        public int? upozilaOrThana { get; set; }
    }
}
