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

    public class EntityIterator<Entity> : IIterator<Entity>
    {
        List<Entity> Entities = new List<Entity>();

        public IOption<Entity> GetNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}