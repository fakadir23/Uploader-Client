﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Common
{
    public class ApiResponse
    {
        public bool? success { get; set; }
        public string message { get; set; }
        public int? errorCode { get; set; }
        public int? code { get; set; }
    }
}
