using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSuscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }//this property will help to have association with MembershipType class model
        public byte MembershipTypeID { get; set; }//will be used as a forign key for MembershipType Id property
    }
}