using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class EnrollmentListSearchResponse : ServiceResponse
    {
        public List<PersonDataDto> passportDataList { get; set; }
        public string total { get; set; }
    }
}
