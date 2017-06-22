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
    public interface IUpdatable { void Update(IUpdateVisitor visitor, float dt); void Update(float dt);}
    public interface IDrawable { void Draw(IDrawVisitor visitor); void Draw(SpriteBatch spriteBatch); }


    interface IComponent : IDrawable, IUpdatable
    {
    }
    public interface IUpdateVisitor
    {
        //void UpdateBar(Bar element, float dt);
        void UpdateBall(Ball element, float dt);
        //void UpdateScore(Score element, float dt);
    }

    public interface IDrawVisitor
    {
        //void DrawBar(Bar element);
        void DrawBalll(Ball element);
        //void DrawScore(Score element);
    }
}