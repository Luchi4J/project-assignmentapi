using ProjectAssignment.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAssignment.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedByEmail { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Status Status { get; set; }
        public string StatusDesc { get; set; }

    }


}
