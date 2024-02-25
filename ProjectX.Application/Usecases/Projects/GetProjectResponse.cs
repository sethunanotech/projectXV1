namespace ProjectX.Application.Usecases.Projects
{
    public class GetProjectResponse
    {
        public Guid ID { get; set; }
        public Guid ClientID { get; set; }
        public string Title { get; set; }
        public string SecretCode { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
