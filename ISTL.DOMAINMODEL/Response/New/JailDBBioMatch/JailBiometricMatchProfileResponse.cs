using ISTL.MODELS.DTO.New.JailDBBioMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.JailDBBioMatch
{
    public class JailBiometricMatchProfileResponse
    {
        public int? errorCode { get; set; }
        public string errorMsg { get; set; }
        public bool? operationResult { get; set; }
        public List<JailProfileDto> profiles { get; set; }
    }
}
