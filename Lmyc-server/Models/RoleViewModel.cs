﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc_server.Models
{
    public class RoleViewModel
    {
        [Key]
        public string RoleId { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public string UserId { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
