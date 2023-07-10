using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public  class CourseInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public decimal? Cost { get; set; }
        public string Schedule { get; set; } = string.Empty;
    }
}
