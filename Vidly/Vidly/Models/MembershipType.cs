using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        // removing magic numbers in our domain
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
        //public static readonly byte Monthly = 2;
        //public static readonly byte Quarterly = 3;
        //public static readonly byte Annual = 4; not needed yet, but you can add it

        // other possible solution: using enum - but you need to cast it to byte to compare it with membershiptype id == extra cast!
    }
}