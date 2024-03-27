using ISTL.MODELS.DTO.New.Enrollment.Adjudication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.Old.Adjudication
{
    public class GetMatchListIdsResponse : ServiceResponse
    {
        public List<MatchResultDto> matchList { get; set; }
        public int? total { get; set; }
    }
}
