using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace webcore_app.Core.Common
{
    [Index(nameof(RowId), IsUnique = true)]
    public  class Auditory
    {
        protected Auditory() 
        {
            CreatedDate = DateTime.Now;
            CreatedUser = "sysadmin";
            RowId = Guid.NewGuid();
        }

        //creation
        [Required]
        public string CreatedUser { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        

        //Update
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [Required]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid RowId { get; set; }

    }
}
