using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Models
{
    public class VerifyAccount
    {
        public int Id { get; set; }
        public string Otp { get; set; }
        public string Userid { get; set; }
        public DateTime? Send_Time { get; set; }
    }
}
