using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Request.New
{
    public class GetBECidentifyRequest
    {
        public bool? isInverted { get; set; }
        public byte[] lt { get; set; }
        public byte[] li { get; set; }
        public byte[] lm { get; set; }
        public byte[] lr { get; set; }
        public byte[] ll { get; set; }
        public byte[] rt { get; set; }
        public byte[] ri { get; set; }
        public byte[] rm { get; set; }
        public byte[] rr { get; set; }
        public byte[] rl { get; set; }
        public byte[] wsqLt { get; set; }
        public byte[] wsqLi { get; set; }
        public byte[] wsqLm { get; set; }
        public byte[] wsqLr { get; set; }
        public byte[] wsqLl { get; set; }
        public byte[] wsqRt { get; set; }
        public byte[] wsqRi { get; set; }
        public byte[] wsqRm { get; set; }
        public byte[] wsqRr { get; set; }
        public byte[] wsqRl { get; set; }
        public string token { get; set; }
    }
}
