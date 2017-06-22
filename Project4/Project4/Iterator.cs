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

    public class EntityManager<Entity> : IIterator<Entity>, IComponent
    {
        public List<Entity> entities;
        private int current = -1;

        public void Draw(IDrawVisitor visitor)
        {
            visitor.DrawEntity(this);
        }

        public void Update(IUpdateVisitor visitor, float dt)
        {
            visitor.UpdateEntity(this, dt);
        }

        public IOption<Entity> GetNext()
        {
            current++;
            if (current > entities.Count)
            {
                return new None<Entity>();
            }
            return new Some<Entity>(entities[current]);
        }

        public void Reset()
        {
            current = -1;
        }
    }


    public abstract class EntityCreator
    {
        public abstract EntityManager<Entity> Instantiate(string option, System.Action exit);
    }

    public class EntityConstructor : EntityCreator
    {
     
        public override EntityManager<Entity> Instantiate(string option, Action exit)
        {
            EntityManager<Entity> entityManager = new EntityManager<Entity>();
            switch (option)
            {
                default:
                    {
                        EntityCreator entityCreator = new EntityFactory();

                    }
            }
        }
    }
}