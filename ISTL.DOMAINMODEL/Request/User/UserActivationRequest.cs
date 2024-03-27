using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.User
{
    public class UserActivationRequest
    {
        public int id { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string username { get; set; }
    }
}
