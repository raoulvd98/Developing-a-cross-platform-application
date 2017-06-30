using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4
{
    /// <summary>
    /// Custom list implemenation and custom enumerator.
    /// Allows developer to create and store lists, and iterate through them.
    /// </summary>
    public interface Iterator<T>
    {
        IOption<T> GetNext();
        IOption<T> GetCurrent();
        void Reset();
    }

    public class List<T> : Iterator<T>
    {
        private int size;
        private T[] array;
        private int current;
        private int amount_of_items;

        public List()
        {
            size = 10;
            amount_of_items = 0;
            current = 0;
            array = new T[10];
            Reset();
        }
        public void Add(T item)
        {
            if (amount_of_items >= size)
            {
                size *= 2;
                T[] new_array = new T[size];
                Array.Copy(array, new_array, amount_of_items);
            }
            else
            {
                array[amount_of_items] = item;
            }
            amount_of_items++;

        }

        public IOption<T> GetNext()
        {
            current++;
            if (current >= amount_of_items)
            {
                return new None<T>();
            }
            return new Some<T>(array[current]);
        }

        public void Reset()
        {
            current = -1;
        }

        public IOption<T> GetCurrent()
        {
            if (current == -1) return new None<T>();
            return new Some<T>(array[current]);
        }
    }
}