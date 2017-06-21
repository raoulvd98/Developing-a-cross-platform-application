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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project4
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
    public interface IUpdatable
    {
//paterpimelottwee
    }

    interface IComponent : IDrawable, IUpdatable
    {
    }
}