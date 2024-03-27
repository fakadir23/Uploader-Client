namespace ISTL.MODELS.DTO.Search
{
    public class SearchCriminalDto
    {
        public string Unit { get; set; }
        public string SubUnit { get; set; }
        public string ReferenceNumber { get; set; }
        public string FullName { get; set; }
        public bool PresentAddress { get; set; }
        public bool PermanentAddress { get; set; }
        public string District { get; set; }
        public string Upazilla { get; set; }
        public string Union { get; set; }
        public string ExportStatus { get; set; }
        public string VillageRoadHouse { get; set; }
        public bool NoDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}