using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Up.Models
{
    public class SachViewModel
    {
        public Guid SachId { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
