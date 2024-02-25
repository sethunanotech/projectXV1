using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.Login
{
    public class ClientLogin
    {
        public Guid ClientID { get; set; }
        public string SecretCode { get; set; }
    }
}
