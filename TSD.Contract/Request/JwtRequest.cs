using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Request
{
    public class JwtRequest
    {
        public int userId { get; set; }
        public string username { get; set; }=string.Empty;
   
    }
}
