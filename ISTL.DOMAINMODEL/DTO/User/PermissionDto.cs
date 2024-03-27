using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.User
{
    public class PermissionDto
    {
        public bool enable { get; set; }
        public int? id { get; set; }
        public string nameEn { get; set; }
        public int? pageOrder { get; set; }
        public int? permissionId { get; set; }
        public int? status { get; set; }
        public int? type { get; set; }
        public string url { get; set; }
        public int? userId { get; set; }
    }
}
