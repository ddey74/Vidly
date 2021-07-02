using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;


namespace Vidly.ViewModel
{
    public class NewCustomerViewModel
    {
        //we can add a list of membership types and customer also and pass this viewmodel to the new() controller
        public IEnumerable<MembershipType> MembershipType { get; set; }
        public Customer Customer { get; set; }
    }
}