using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class BSTNode
    {
        public ServiceRequest Data { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequest data)
        {
            Data = data;
        }
    }
}
