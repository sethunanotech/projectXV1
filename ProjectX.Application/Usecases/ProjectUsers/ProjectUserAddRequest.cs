using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.ProjectUsers
{
    public class ProjectUserAddRequest
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public string? CreatedBy { get; set; }
    }
}
