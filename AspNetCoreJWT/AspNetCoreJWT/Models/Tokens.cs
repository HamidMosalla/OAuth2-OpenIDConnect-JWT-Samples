using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreJWT.Models
{
    public class Tokens
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}