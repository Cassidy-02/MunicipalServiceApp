using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class Issue
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime DateReported { get; set; }

        public override string ToString()
        {
            return $"Category: {Category}, Description: {Description}, Location: {Location}, DateReported: {DateReported}";
        }
    }
}
