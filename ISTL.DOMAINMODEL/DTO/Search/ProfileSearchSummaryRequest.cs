namespace ISTL.MODELS.DTO.Search
{
    public class ProfileSearchSummaryRequest
    {
        public ProfileSearchSummaryRequest(SearchCriminalDto searchCriminalDto)
        {
            referenceNo = searchCriminalDto.ReferenceNumber;
            fullName = searchCriminalDto.FullName;
            creationDateFrom = null;
            creationDateTo = null;
        }

        public ProfileSearchSummaryRequest(string referenceNo)
        {
            this.referenceNo = referenceNo;
        }

        public ProfileSearchSummaryRequest()
        {

        }
        public string creationDateFrom { get; set; }
        public string creationDateTo { get; set; }

        public int? crimeType { get; set; }
        public string fullName { get; set; }
        public string id { get; set; }
        public int? limit { get; set; }
        public PermanentAddress permanentAddress { get; set; }
        public PresentAddress presentAddress { get; set; }
        public string referenceNo { get; set; }
        public int? startIndex { get; set; }
        public int? unit { get; set; }

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
    }
}