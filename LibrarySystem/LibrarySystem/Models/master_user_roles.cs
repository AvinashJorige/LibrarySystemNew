using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class master_user_roles
    {
        public System.Guid user_role_id { get; set; }
        public string user_role_name { get; set; }
        public DateTime user_role_created_date { get; set; }
        public DateTime user_role_updated_date { get; set; }
    }
}
