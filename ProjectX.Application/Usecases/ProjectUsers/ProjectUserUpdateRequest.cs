using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.ProjectUsers
{
    public class ProjectUserUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ProjectID { get; set; }
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public string? LastModifiedBy { get; set; }
    }
}
