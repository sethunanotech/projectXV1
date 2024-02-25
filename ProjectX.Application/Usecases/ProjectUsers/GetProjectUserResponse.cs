namespace ProjectX.Application.Usecases.ProjectUsers
{
    public class GetProjectUserResponse
    {
        public Guid ID { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? LastModifiedBy { get; set; }
    }
    public class AddProjectUserRequest
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }

    }
    public class UpdateProjectUserRequest
    {
        public Guid Id { get; set; }
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
    }
}
