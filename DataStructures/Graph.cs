using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class Graph
    {
        private Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

        public void AddEdge(int from, int to)
        {
            if (!adjList.ContainsKey(from))
                adjList[from] = new List<int>();
            adjList[from].Add(to);
        }

        public List<int> GetDependencies(int requestID)
        {
            return adjList.ContainsKey(requestID) ? adjList[requestID] : new List<int>();
        }
    }
}
