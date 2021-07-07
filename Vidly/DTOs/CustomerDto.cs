using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSuscribedToNewsLetter { get; set; }
        //public MembershipType MembershipType { get; set; } excluding as it is a domain class and it is creating the dependency
        //we must have all properties decoupled from our domain class
        //[Display(Name = "Membership Type")]//not required because we use them to render form
        public byte MembershipTypeID { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}