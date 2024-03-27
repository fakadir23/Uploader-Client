using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.User
{
    public class UserSearchCriteriaRequest
    {
        public string designationEn { get; set; }
        public string email { get; set; }
        public int? id { get; set; }
        public int? limit { get; set; }
        public string nameBn { get; set; }
        public string nameEn { get; set; }
        public string order { get; set; }
        public string phone { get; set; }
        public string sortColumnName { get; set; }
        public int? startIndex { get; set; }
        public int? status { get; set; }
        public int? type { get; set; }
        public string username { get; set; }
    }
}
