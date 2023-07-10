using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public class MonthFeeInfo
    {
        public string Name { get; set; } = String.Empty;
        public DateTime Month { get; set; } = DateTime.Now;
        public int Fees { get; set; }
        public bool IsPaid { get; set; } = false;
        public int StudentId { get; set; }
    }
}
