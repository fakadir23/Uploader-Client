﻿using ISTL.MODELS.DTO.New.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New.Lookup
{
    public class GetGeoMapListResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public List<RabGeoMapDto> rabGeoMapList { get; set; }
        public int? total { get; set; }
    }
}
