using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public  class StudentInfo
    {
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string StudentGrade { get; set; } = string.Empty;
        public int? StudentRate { get; set; }
        public int CourseID { get; set; }
    }
}
