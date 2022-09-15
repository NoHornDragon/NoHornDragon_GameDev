using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


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
    private T[] items;
    private int curCount;
    public int Count { get { return curCount; } }

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void  Add(T item)
    {
        item.HeapIndex = curCount;
        items[curCount] = item;
        SortUp(item);
        curCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        curCount--;
        items[0] = items[curCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);

        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    private void SortDown(T item)
    {
        while(true)
        {
            int leftChild = item.HeapIndex * 2 + 1;
            int rightChild = item.HeapIndex * 2 + 2;

            int swapIndex = 0;

            if(leftChild < curCount){
                swapIndex = leftChild;

                if(rightChild < curCount)
                {
                    if(items[leftChild].CompareTo(items[rightChild]) < 0)
                    {
                        swapIndex = rightChild;
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
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
        while(true)
        {
            T parentItem = items[parent];
            if(item.CompareTo(parentItem) > 0)
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
        items[A.HeapIndex] = B;
        items[B.HeapIndex] = A;

        int aIndex = A.HeapIndex;
        A.HeapIndex = B.HeapIndex;
        B.HeapIndex = aIndex;
    }
}
