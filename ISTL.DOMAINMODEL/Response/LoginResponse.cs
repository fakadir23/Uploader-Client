using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response
{
    public class LoginResponse
    {
        public int? id { get; set; }
        public string token { get; set; }
        public string userName { get; set; }
        public bool? operationResult { get; set; }
        public int? errorCode { get; set; }
        public string errorMsg { get; set; }
        public string expiredIn { get; set; }
        public UserStatus status { get; set; }

        public LoginResponse()
        {

        }
    }
}
