using ProjectAssignment.Domain.Common;
using ProjectAssignment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectAssignment.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
