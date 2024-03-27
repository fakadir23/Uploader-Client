using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Lookup
{
    public class UnionDto
    {
        public bool active { get; set; }
        public string code { get; set; }
        public string createdBy { get; set; }
        public string creationDate { get; set; }
        public bool deleted { get; set; }
        public int? id { get; set; }
        public string modificationDate { get; set; }
        public string modifiedBy { get; set; }
        public string nameInBangla { get; set; }
        public string nameInEnglish { get; set; }
        public int? upazillaId { get; set; }
    }
}
