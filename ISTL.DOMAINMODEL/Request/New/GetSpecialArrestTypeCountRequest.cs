using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.New
{
    public class GetSpecialArrestTypeCountRequest
    {
        public string countDate { get; set; }
        public int limit { get; set; }
        public int startIndex { get; set; }
        public int? subUnit { get; set; }
        public int? unit { get; set; }
    }
}
