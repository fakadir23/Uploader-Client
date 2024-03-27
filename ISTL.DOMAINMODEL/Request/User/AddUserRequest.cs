using ISTL.MODELS.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.User
{
    public class AddUserRequest
    {
        public string email { get; set; }
        public int? id { get; set; }
        public string nameBn { get; set; }
        public string nameEn { get; set; }
        public string password { get; set; }
        public List<PermissionDto> permissions { get; set; }
        public string phone { get; set; }
        public int? subunit { get; set; }
        public string subUnitName { get; set; }
        public int? type { get; set; }
        public int? role { get; set; }
        public string roleName { get; set; }
        public int? unit { get; set; }
        public string unitName { get; set; }
        public bool userActivated { get; set; }
        public string username { get; set; }
        public string workStationCode { get; set; }
        public bool status { get; set; }

        public AddUserRequest()
        {
            permissions = new List<PermissionDto>();
        }
    }
}
