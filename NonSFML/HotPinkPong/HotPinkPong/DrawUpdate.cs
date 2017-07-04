using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Project4
{
    /// <summary>
    /// Implements the IDrawable and IUpdatable for easy access.
    /// </summary>
    public interface IDrawUpdate : IDrawable, IUpdatable { }

    /// <summary>
    /// Define interfaces to allow for drawing entities and the screen.
    /// </summary>
    public interface IDrawingManager
    {
        void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color, string sprite_name);
    }
    public interface IDrawable { void Draw(IDrawVisitor visitor); }
    public interface IDrawVisitor
    {
        void DrawScreen(EntityManager entityManager);
        void DrawEntity(Entity entity);
    }
    public enum Colour { White, Hotpink };

    /// <summary>
    /// Define interfaces to allow for updating entities and screen.
    /// </summary>
    public interface IUpdateVisitor
    {
        void UpdateScreen(EntityManager entityManager, float dt);
        void UpdateEntity(Entity entity, float dt);
    }
    public interface IUpdatable { void Update(IUpdateVisitor visitor, float dt); }

    /// <summary>
    /// Concrete implementations for drawing entities on the screen.
    /// </summary>
    public class MonogameDrawingAdapter : IDrawingManager
    {
        SpriteBatch sprite_batch;
        ContentManager content_manager;
        Texture2D number_0;
        Texture2D number_1;
        Texture2D number_2;
        Texture2D number_3;
        Texture2D number_4;
        Texture2D number_5;
        Texture2D white_pixel;

        public MonogameDrawingAdapter(SpriteBatch sprite_batch, ContentManager content_manager)
        {
            this.sprite_batch = sprite_batch;
            this.content_manager = content_manager;
            white_pixel = content_manager.Load<Texture2D>("white_pixel");
            number_0 = content_manager.Load<Texture2D>("number_0");
            number_1 = content_manager.Load<Texture2D>("number_1");
            number_2 = content_manager.Load<Texture2D>("number_2");
            number_3 = content_manager.Load<Texture2D>("number_3");
            number_4 = content_manager.Load<Texture2D>("number_4");
            number_5 = content_manager.Load<Texture2D>("number_5");
        }

        private Microsoft.Xna.Framework.Color Convert_color(Colour color)
        {
            switch (color)
            {
                case Colour.White:
                    return Microsoft.Xna.Framework.Color.White;
                case Colour.Hotpink:
                    return Microsoft.Xna.Framework.Color.HotPink;
                default:
                    return Microsoft.Xna.Framework.Color.White;
            }
        }

        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color, string sprite_name)
        {
            switch (sprite_name)
            {
                case "white_pixel":
                    sprite_batch.Draw(white_pixel, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_0":
                    sprite_batch.Draw(number_0, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_1":
                    sprite_batch.Draw(number_1, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_2":
                    sprite_batch.Draw(number_2, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_3":
                    sprite_batch.Draw(number_3, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_4":
                    sprite_batch.Draw(number_4, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                case "number_5":
                    sprite_batch.Draw(number_5, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), Convert_color(color));
                    break;
                default:
                    throw new Exception(sprite_name);
            }
        }
    }

    /// <summary>
    /// Concrete implementation for visiting the list and drawing.
    /// </summary>
    public class DefaultDrawVisitor : IDrawVisitor
    {
        IDrawingManager drawing_manager;

        public DefaultDrawVisitor(IDrawingManager drawing_manager)
        {
            this.drawing_manager = drawing_manager;
        }

        public void DrawEntity(Entity entity)
        {
            drawing_manager.DrawRectangle(new Point(Convert.ToInt32(entity.Position.X), Convert.ToInt32(entity.Position.Y)), entity.width, entity.height, Colour.Hotpink, entity.sprite_name);
        }

        public void DrawScreen(EntityManager entityManager)
        {
            entityManager.entities.Reset();
            while (entityManager.entities.GetNext().Visit(() => false, _ => true))
            {
                entityManager.entities.GetCurrent().Visit(() => { }, item => { item.Draw(this); });
            }
        }
    }
    public class DefaultUpdateVisitor : IUpdateVisitor
    {
        InputManager input_manager;
        EntityConstructor constructor;

        public DefaultUpdateVisitor(InputManager input_manager, EntityConstructor constructor)
        {
            this.input_manager = input_manager;
            this.constructor = constructor;
        }

        public void UpdateEntity(Entity entity, float dt)
        {
            entity.Checkcollision(constructor.PaddleLeft, constructor.PaddleRight);
            entity.ChangeVelocity(input_manager, dt, constructor.Ball);
            entity.CheckOutOfBounds(constructor.PaddleLeft, constructor.PaddleRight, constructor.ScoreLeft, constructor.ScoreRight);
        }

        public void UpdateScreen(EntityManager entityManager, float dt)
        {
            entityManager.entities.Reset();
            while (entityManager.entities.GetNext().Visit(() => false, _ => true))
            {
                entityManager.entities.GetCurrent().Visit(() => { }, item => { item.Update(this, dt); });
            }
        }
    }
}