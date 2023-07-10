using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public class AttendanceInfo
    {
        public int SessionNumber { get; set; }
        public int StudentId { get; set; }
        public bool AttendanceStatus { get; set; } = false;

    }
}
