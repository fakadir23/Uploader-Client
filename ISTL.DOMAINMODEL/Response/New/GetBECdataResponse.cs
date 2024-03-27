using ISTL.MODELS.DTO.New.Enrollment.BECverify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.Response.New
{
    public class GetBECdataResponse
    {
        public string bloodGroup { get; set; }
        public string dateOfBirth { get; set; }
        public string father { get; set; }
        public List<FingerUploadUrlDto> fingerUploadUrls { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string mother { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
        public string nationalId { get; set; }
        public string nidFather { get; set; }
        public string nidMother { get; set; }
        public BECAddressDto permanentAddress { get; set; }
        public byte[] photo { get; set; }
        public string pin { get; set; }
        public BECAddressDto presentAddress { get; set; }
        public string religion { get; set; }
        public string requestId { get; set; }
        public string voterArea { get; set; }
        public string voterAreaCode { get; set; }
    }
}
