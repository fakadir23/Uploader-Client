using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New
{
    public class DateTimeInfoDto
    {
        public string date { get; set; }
        public string day { get; set; }
        public string hours { get; set; }
        public string minutes { get; set; }
        public string month { get; set; }
        public string nanos { get; set; }
        public string seconds { get; set; }
        public string time { get; set; }
        public string timezoneOffset { get; set; }
        public string year { get; set; }
    }
}
