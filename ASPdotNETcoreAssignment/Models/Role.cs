using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPdotNETcoreAssignment.Models
{
    public class Role
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        [Range(1, 100, ErrorMessage = "Role Name have to be between 1 - 100 letters")]
        public string Name { get; set; }
    }
}
