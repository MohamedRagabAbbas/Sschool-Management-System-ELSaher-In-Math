﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public class UserLogInInfo
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
        public bool IsAdmin { get; set; } = false;

    }
}