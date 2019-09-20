using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Data.Models
{
    public class UserStatus
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public Nullable<DateTime> LoggedInDate { get; set; }
        public Nullable<DateTime> LoggedOutDate { get; set; }
    }
}
