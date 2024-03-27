using ISTL.MODELS.DTO.New.NotEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.NotEntry
{
    public class NotEntrySearchResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public List<NotEntryDto> noEntryList { get; set; }
        public int? total { get; set; }
    }
}
