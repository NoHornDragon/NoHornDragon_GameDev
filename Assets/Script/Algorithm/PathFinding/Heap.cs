using System;

namespace NHD.Algorithm.PathFinding
{
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex
        {
            get;
            set;
        }
    }

    public class Heap<T> where T : IHeapItem<T>
    {
        private T[] _items;
        private int _curCount;
        public int Count { get { return _curCount; } }

        public Heap(int maxHeapSize)
        {
            _items = new T[maxHeapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = _curCount;
            _items[_curCount] = item;
            SortUp(item);
            _curCount++;
        }

        public T RemoveFirst()
        {
            T firstItem = _items[0];
            _curCount--;
            _items[0] = _items[_curCount];
            _items[0].HeapIndex = 0;
            SortDown(_items[0]);

            return firstItem;
        }

        public void UpdateItem(T item)
        {
            SortUp(item);
        }

        public bool Contains(T item)
        {
            return Equals(_items[item.HeapIndex], item);
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int leftChild = item.HeapIndex * 2 + 1;
                int rightChild = item.HeapIndex * 2 + 2;

                int swapIndex = 0;

                if (leftChild < _curCount)
                {
                    swapIndex = leftChild;

                    if (rightChild < _curCount)
                    {
                        if (_items[leftChild].CompareTo(_items[rightChild]) < 0)
                        {
                            swapIndex = rightChild;
                        }
                    }

                    if (item.CompareTo(_items[swapIndex]) < 0)
                    {
                        Swap(item, _items[swapIndex]);
                    }
                    else
                        return;
                }
                else
                {
                    return;
                }
            }
        }

        private void SortUp(T item)
        {
            int parent = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parentItem = _items[parent];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parent = (item.HeapIndex - 1) / 2;
            }
        }

        private void Swap(T A, T B)
        {
            _items[A.HeapIndex] = B;
            _items[B.HeapIndex] = A;

            int aIndex = A.HeapIndex;
            A.HeapIndex = B.HeapIndex;
            B.HeapIndex = aIndex;
        }
    }
}