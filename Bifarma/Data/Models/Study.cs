namespace Bifarma.Data.Models
{
    public class Study
    {
        public string Id { get; set; }
        public string StudyId { get; set; }
        public string VersionId { get; set; }
        public string ProtocolTitle { get; set; }
        public string MoleculesID { get; set; }
        public string StudyStatusID { get; set; }
        public string isActive { get; set; }
        public string isDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set;}
        public string State { get; set; }
    }
}
