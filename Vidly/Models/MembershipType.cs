using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public short SignUpFee { get; set; }
        public byte DurationInMonth { get; set; }
        public byte DiscountRate { get; set; }
        public byte ID {get; set;}

        //Need to have an assosciation with customer class
    }
}