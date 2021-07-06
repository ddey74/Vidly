using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        //we can apply data anotations to override default conventions of entity framework
        //like by default entity framework will apply string name as nvarchar and nullable
        //after making changes in data annotations we need to push those changes to table using migrations
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSuscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }//this property will help to have association with MembershipType class model
        [Display(Name="Membership Type")]
        public byte MembershipTypeID { get; set; }//will be used as a forign key for MembershipType Id property

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}