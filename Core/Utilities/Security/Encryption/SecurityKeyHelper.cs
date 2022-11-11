using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{

    // işin içinde şifreleme olan her sistemde her şeyi byte array formatında veriyor olmamız lazım  
    public class SecurityKeyHelper
    {

        public static SecurityKey CreateSecurityKey(string securityKey)
        { 
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));   
        }
    }
}
