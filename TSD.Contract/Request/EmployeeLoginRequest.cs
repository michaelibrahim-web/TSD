using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Contract.Request
{
    public class EmployeeLoginRequest
    {

        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
