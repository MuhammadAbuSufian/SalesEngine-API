﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RequestModel
{
    public class UserUpdateRequestModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string RoleId { get; set; }
        public string Password { get; set; }
        public string CompanyId { get; set; }
        
    }
}