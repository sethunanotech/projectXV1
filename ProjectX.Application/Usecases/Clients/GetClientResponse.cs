namespace ProjectX.Application.Usecases.Clients
{
    public class GetClientResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
