using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class GetEnrollmentDetailsResponse : ServiceResponse
    {
        public PersonDataDto passportData { get; set; }
    }
}
