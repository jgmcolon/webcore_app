using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webcore_app.Core.Common;

namespace webnet_app.domain.Entities.Permission
{
    [Table("Permission", Schema = "Permission")]
    public class Permission : Auditory
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }



        [ForeignKey("PermissionType")]
        public int PermissionTypeId { get; set; }
        public virtual PermissionType PermissionType { get; set; }


        [Required]
        public DateTime PermissionDate { get; set; }


    }
}
