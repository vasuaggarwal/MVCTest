using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model
{
    public class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal EstimatedCost { get; set; }
        public DateTime Dated { get; set; }
        public Priority Priority { get; set; }
    }
    public enum Priority
    {
        Priority1 = 1,
        Priority2 = 2,
        Priority3 = 3,
    }
}
