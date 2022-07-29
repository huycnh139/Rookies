using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.System.Users
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool Rememberme { get; set; }
    }
}
