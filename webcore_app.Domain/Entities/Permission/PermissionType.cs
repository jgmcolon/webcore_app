using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webcore_app.Core.Common;

namespace webnet_app.domain.Entities.Permission
{
    [Table("PermissionType", Schema = "Permission")]
    public class PermissionType : Maintenance
    {

        public PermissionType()
        {
            Permissions = new HashSet<Permission>();
        }


        public virtual ICollection<Permission> Permissions { get; set; }


    }
}
