using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Enrollment
{
    public class PersonDataDto
    {
        public string afisStatus { get; set; }
        public string alias { get; set; }
        public string createdBy { get; set; }
        public string createdOn { get; set; }
        public string dateOfBirth { get; set; }
        public string faceEnrolled { get; set; }
        public string fpEnrolled { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public int? id { get; set; }
        public string irisEnrolled { get; set; }
        public string lastTransactionId { get; set; }
        public int? matchScore { get; set; }
        public string nationalId { get; set; }
        public string nickName { get; set; }
        public string occupation { get; set; }
        public PersonBiometricDto personBiometric { get; set; }
        public string phone { get; set; }
        public string referenceNumber { get; set; }
        public string stationId { get; set; }
        public string transactionType { get; set; }
        public string updatedBy { get; set; }
        public string updatedOn { get; set; }
        public string uploadDate { get; set; }
    }
}
