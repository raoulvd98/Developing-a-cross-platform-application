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
    public interface IUpdatable { void Update(IUpdateVisitor visitor, float dt);}
    public interface IDrawable { void Draw(IDrawVisitor visitor);}


    interface IComponent : IDrawable, IUpdatable
    {
    }
    public interface IUpdateVisitor
    {
        //void UpdateScore(Score element, float dt);
        void UpdateEntity<Entity>(EntityManager<Entity> entityManager, float dt);
    }

    public interface IDrawVisitor
    {
        //void DrawBar(Bar element);
        //void DrawScore(Score element);
        void DrawEntity<Entity>(EntityManager<Entity> entityManager);
    }
}