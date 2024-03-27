using ISTL.MODELS.Common;
using System;
using System.Collections.Generic;

namespace ISTL.MODELS.Response.New
{
    public class ProfileSearchSummaryResponse : ApiResponse
    {
        public List<CriminalProfileSummaryList> criminalProfileSummaryList { get; set; }
        public int? total { get; set; }
    }

    public class CriminalProfileSummaryList
    {
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public string dateOfBirth { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string id { get; set; }
        public Nationality nationality { get; set; }
        public List<string> nickName { get; set; }
        public string occupation { get; set; }
        public PermanentAddress permanentAddress { get; set; }
        public PresentAddress presentAddress { get; set; }
        public string referenceNo { get; set; }
        public string religion { get; set; }
        public int? unit { get; set; }
    }

    public class PermanentAddress
    {
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public int? district { get; set; }
        public int? division { get; set; }
        public int? id { get; set; }
        public int? union { get; set; }
        public int? upazila { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public string villageHouseRoadNo { get; set; }
    }

    public class PresentAddress
    {
        public string createdAt { get; set; }
        public int? createdBy { get; set; }
        public int? district { get; set; }
        public int? division { get; set; }
        public int? id { get; set; }
        public int? union { get; set; }
        public int? upazila { get; set; }
        public string updatedAt { get; set; }
        public int? updatedBy { get; set; }
        public string villageHouseRoadNo { get; set; }
    }

    public class Nationality
    {
        public bool? byBirth { get; set; }
        public bool? byDomicile { get; set; }
        public int? id { get; set; }
    }

}