using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.Entity
{
    public class GetEntityResponse
    {
        public Guid Id { get; set; }
        public Guid ProjectID { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
        public string DisplayName { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Argument1 { get; set; }
        public string Argument2 { get; set; }
        public string Argument3 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
