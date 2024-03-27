using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Lookup
{
    public class NationalityDto
    {
        public bool byBirth { get; set; }
        public bool byDomicile { get; set; }
        public string code { get; set; }
        public string countryNameEn { get; set; }
        public string countryNameLocal { get; set; }
        public string countryNationality { get; set; }
        public string countryIcaoName { get; set; }
        public int? status { get; set; }
        public int? id { get; set; }
    }
}
