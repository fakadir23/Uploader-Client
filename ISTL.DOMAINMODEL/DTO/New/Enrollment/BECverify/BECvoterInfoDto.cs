using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.New.Enrollment.BECverify
{
    public class BECvoterInfoDto
    {
        public int id { get; set; }
        public string bloodGroup { get; set; }
        public string dob { get; set; }
        public DateTime dobDt { get; set; }
        public string disability { get; set; }
        public string drivingLicense { get; set; }
        public string education { get; set; }
        public string father { get; set; }
        public string formNo { get; set; }
        public List<string> fpNames { get; set; }
        public string gender { get; set; }
        public string identificationMark { get; set; }
        public string maritalStatus { get; set; }
        public string matchPercentage { get; set; }
        public string mobile { get; set; }
        public string mother { get; set; }
        public string name { get; set; }
        public string nameEn { get; set; }
        public string nid { get; set; }
        public string occupation { get; set; }
        public string passportNumber { get; set; }
        public string phone { get; set; }
        public byte[] photo { get; set; }
        public string placeOfBirth { get; set; }
        public string religion { get; set; }
        public double score { get; set; }
        public string slno { get; set; }
        public string spouse { get; set; }
        public string tin { get; set; }
        public string voterArea { get; set; }
        public string voterAreaCode { get; set; }
        public string voterNo { get; set; }
        public string permanentAddress { get; set; }
        public string presentAddress { get; set; }
        public string token { get; set; }
        public int status { get; set; }
        public int userId { get; set; }
        public string createdAt { get; set; }
        public string resultFoundAt { get; set; }

        public string createdAtCustom { get; set; }
        public string resultFoundAtCustom { get; set; }


        public DateTime createdAtDt { get; set; }
        public DateTime resultFoundAtDt { get; set; }
    }
}
