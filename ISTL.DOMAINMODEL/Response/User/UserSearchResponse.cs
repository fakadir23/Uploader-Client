using ISTL.MODELS.Common;
using ISTL.MODELS.DTO.User;
using ISTL.MODELS.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.User
{
    public class UserSearchResponse : ApiResponse
    {
        public int? totalCount { get; set; }
        public List<AddUserRequest> userList { get; set; }
    }
}
