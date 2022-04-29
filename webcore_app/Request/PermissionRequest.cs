using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webcore_app.Request
{
    public class PermissionRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
        public string RowId { get; set; }

      

    }
}
