using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project4
{
    public interface IIterator<T>
    {
        IOption<T> GetNext();
        void Reset();
    }

    public class EntityManager : IComponent
    {
        public List<Entity> entities;

        public void Draw(IDrawVisitor visitor)
        {
            visitor.DrawEntity(this);
        }

        public void Update(IUpdateVisitor visitor, float dt)
        {
            visitor.UpdateEntity(this, dt);
        }

    }


    public class EntityConstructor
    {
     
        public EntityManager Instantiate(string option, Action exit)
        {
            EntityManager entityManager = new EntityManager();
            switch (option)
            {
                default:
                    {
                        EntityFactory entityCreator = new EntityFactory();
                        entityManager.entities = new List<Entity>();
                        entityManager.entities.Add(entityCreator.Create(0));
                        entityManager.entities.Add(entityCreator.Create(1));
                        entityManager.entities.Add(entityCreator.Create(2));

                        break;
                    }                                   
            }
            return entityManager;
        }
    }

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