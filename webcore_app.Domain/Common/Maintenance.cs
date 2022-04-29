using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace webcore_app.Core.Common
{
    public class Maintenance : Auditory
    {

        protected Maintenance()
        {
            Active = true;
            Deleted = false;
            ByDefault = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }


        [Required]
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool ByDefault { get; set; }
        
    }
}
