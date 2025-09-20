using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class PriorityQueue<T>
    {
        private List<(T Item, int Priority)> _elements = new List<(T, int)>();

        public int Count => _elements.Count;

        // Add item with given priority
        public void Enqueue(T item, int priority)
        {
            _elements.Add((item, priority));
            _elements = _elements.OrderBy(e => e.Priority).ToList();
        }

        //Remove and return the item with the highest priority (smallest number = highest priority)
        public T Dequeue()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            int bestIndex = 0;
            for (int i = 1; i < _elements.Count; i++)
            {
                if (_elements[i].Priority < _elements[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }

            T bestItem = _elements[bestIndex].Item;
            _elements.RemoveAt(bestIndex);
            return bestItem;
        }

        //Peek at the next item without removing it 
        public T Peek()
        {
            if (_elements.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            int bestIndex = 0;
            for (int  i = 1;  i < _elements.Count;  i++)
            {
                if (_elements[i].Priority < _elements[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }
            return _elements[bestIndex].Item;
        }

        // Snapshot of current items (used for display in ListBox without modifying the queue)
        public List<(T Item, int Priority)> Snapshot()
        {
            return new List<(T, int)>(_elements);
        }
    }
}
