using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.Login
{
   
        public class TokenResponse
        {
            public string authSigningKey { get; set; }
            public string issuer { get; set; }
            public string audience { get; set; }
            public string validinminutes { get; set; }
            public List<Claim> Claims { get; set; }
        }

}
