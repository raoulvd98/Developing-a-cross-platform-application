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
        void UpdateEntity(EntityManager entityManager, float dt);
    }

    public interface IDrawVisitor
    {
        //void DrawBar(Bar element);
        //void DrawScore(Score element);
        void DrawEntity(EntityManager entityManager);
        void DrawBall(Entity entityBall);
    }

    public class DefaultDrawVisitor : IDrawVisitor
    {
        IDrawingManager drawing_manager;

        public DefaultDrawVisitor(IDrawingManager drawing_manager)
        {
            this.drawing_manager = drawing_manager;
        }

        public void DrawBall (Entity entity)
        {
            drawing_manager.DrawRectangle(new Point(entity.Rectangle.X, entity.Rectangle.Y), entity.Rectangle.Width, entity.Rectangle.Height, Colour.White);
        }

        public void DrawPaddle(Entity entity)
        {
            drawing_manager.DrawRectangle(new Point(entity.Rectangle.X, entity.Rectangle.Y), entity.Rectangle.Width, entity.Rectangle.Height, Colour.White);
        }

        public void DrawEntity(EntityManager entityManager)
        {
            entityManager.entities.Reset();
            while (entityManager.entities.GetNext().Visit(() => false, _ => true))
            {
                entityManager.entities.GetCurrent().Visit(() => { }, item => { item.Draw(this); });
            }   
        }
    }
}