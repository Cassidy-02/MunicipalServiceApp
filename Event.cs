using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Name} ({Category}) @ {Location}";
          
        }
    }
}
