using System.ComponentModel.DataAnnotations;

namespace ProjectX.Application.Usecases.Projects
{
    public class ProjectAddRequest
    {
       
        public Guid ClientID { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
    }
}
