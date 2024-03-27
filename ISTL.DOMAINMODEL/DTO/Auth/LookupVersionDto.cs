using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Auth
{
    public class LookupVersionDto
    {
        public int? id { get; set; }
        public int? station { get; set; }
        public int? subStation { get; set; }
        public int? division { get; set; }
        public int? district { get; set; }
        public int? upazila { get; set; }
        public int? union { get; set; }
        public int? nationality { get; set; }
        public int? policeStation { get; set; }
        public int? rabGeoMap { get; set; }
        public int? rabDistrict { get; set; }
        public int? rabUpazila { get; set; }
        public int? lookupData { get; set; }
        public int? crimeType { get; set; }
    }
}
