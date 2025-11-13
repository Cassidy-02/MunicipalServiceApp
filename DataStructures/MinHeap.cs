using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp
{
    public class MinHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

       public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;

            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return min;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[index].Priority >= heap[parent].Priority) break;
                (heap[index], heap[parent]) = (heap[parent], heap[index]);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int left, right, smallest;
            while (true)
            {
                left = 2 * index + 1;
                right = 2 * index + 2;
                smallest = index;

                if (left < heap.Count && heap[left].Priority < heap[smallest].Priority)
                    smallest = left;
                if (right < heap.Count && heap[right].Priority < heap[smallest].Priority)
                    smallest = right;
                if (smallest == index) break;

                (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                index = smallest;
            }
        }

        public List<ServiceRequest> GetAll() => heap.ToList();
    }
}
