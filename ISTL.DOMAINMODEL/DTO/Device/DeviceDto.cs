using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Device
{
    public class DeviceDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public string[] Type { get; set; }
        public string[] SupportedModel { get; set; }
    }
}
