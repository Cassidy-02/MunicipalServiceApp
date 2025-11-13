using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class ServiceRequest 
    {
        public int RequestID { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        public DateTime DateSubmitted { get; set; }

        public int Priority { get; set; } // 1 (Low) to 5 (High)

        public override string ToString()
        {
            return $"{RequestID}: {RequestType} - {Status} ({DateSubmitted:yyyy-MM-dd}, Priority {Priority})";
        }
    }
}
