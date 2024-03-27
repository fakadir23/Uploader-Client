using ISTL.MODELS.DTO.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.Adjudication
{
    public class GetMatchListResponse : ServiceResponse
    {
        public List<PersonDataDto> passportDataList { get; set; }
        public int? total { get; set; }
    }
}
