namespace ProjectX.Application.Usecases.Package
{
    public class GetPackageResponse
    {
        public Guid ID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid EntityID { get; set; }
        public int Version { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
