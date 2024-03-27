using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Lookup
{
    public class RecoveryDto
    {
        public int id { get; set; }
        public string lookupNameEn { get; set; }
        public string lookupType { get; set; }
        public int? shortId { get; set; }
    }
}
