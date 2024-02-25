using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.VersionManagement
{
    public class GetUpdateResponse
    {
        public int Version { get; set; }
        public string Url { get; set; }
    }
}
