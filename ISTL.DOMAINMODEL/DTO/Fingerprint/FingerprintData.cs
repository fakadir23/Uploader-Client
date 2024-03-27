using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Fingerprint
{
    public class FingerprintData
    {
        public byte[] FpRt { get; set; }
        public byte[] WsqRt { get; set; }
        public byte[] FpRi { get; set; }
        public byte[] WsqRi { get; set; }
        public byte[] FpRm { get; set; }
        public byte[] WsqRm { get; set; }
        public byte[] FpRr { get; set; }
        public byte[] WsqRr { get; set; }
        public byte[] FpRl { get; set; }
        public byte[] WsqRl { get; set; }

        public byte[] FpLt { get; set; }
        public byte[] WsqLt { get; set; }
        public byte[] FpLi { get; set; }
        public byte[] WsqLi { get; set; }
        public byte[] FpLm { get; set; }
        public byte[] WsqLm { get; set; }
        public byte[] FpLr { get; set; }
        public byte[] WsqLr { get; set; }
        public byte[] FpLl { get; set; }
        public byte[] WsqLl { get; set; }
    }
}
