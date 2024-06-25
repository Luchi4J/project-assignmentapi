using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectAssignment.Application.Common.Models
{
    public class UserEvent
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
