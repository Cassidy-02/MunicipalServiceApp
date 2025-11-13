using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class BinarySearchTree
    {
        public BSTNode Root { get; private set; }

        public void Insert(ServiceRequest request)
        {
            Root = InsertRecursive(Root, request);
        }

        private BSTNode InsertRecursive(BSTNode node, ServiceRequest request)
        {
            if (node == null)
            {
                return new BSTNode(request);
            }
            if (request.RequestID < node.Data.RequestID)
            {
                node.Left = InsertRecursive(node.Left, request);
            }
            else if (request.RequestID > node.Data.RequestID)
            {
                node.Right = InsertRecursive(node.Right, request);
            }
            return node;
        }

        public ServiceRequest Search(int id)
        {
            var node = SearchRecursive(Root, id);
            return node?.Data;
        }

        private BSTNode SearchRecursive(BSTNode node, int id)
        {
            if (node == null || node.Data.RequestID == id)
            {
                return node;
            }
            if (id < node.Data.RequestID)
            {
                return SearchRecursive(node.Left, id);
            }
            return SearchRecursive(node.Right, id);
        }

        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> list = new List<ServiceRequest>();
            InOrder(Root, list);
            return list;
        }

        private void InOrder(BSTNode node, List<ServiceRequest> list)
        {
            if (node == null)
            {
                return;
            }
            InOrder(node.Left, list);
            list.Add(node.Data);
            InOrder(node.Right, list);
        }
    }
}
