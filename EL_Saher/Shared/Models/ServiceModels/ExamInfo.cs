using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
    public class ExamInfo
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Score { get; set; }
        public decimal OutOf { get; set; }
        public int StudentId { get; set; }
    }
}
