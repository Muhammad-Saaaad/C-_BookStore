using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAPI.Models
{
    public class Accounts
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string user_type { get; set; }
    }
}