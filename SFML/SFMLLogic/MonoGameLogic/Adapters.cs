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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using SharedLogic;

namespace MonoGameLogic
{
    /// <summary>
    /// Draw entities in the logic of SFML.
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

        public void DrawRectangle(SharedLogic.Point top_left_coordinate, float width, float height, Colour color, string sprite_name)
        {
            switch (sprite_name)
            {
                case "white_pixel":
                    sprite_batch.Draw(white_pixel, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_0":
                    sprite_batch.Draw(number_0, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_1":
                    sprite_batch.Draw(number_1, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_2":
                    sprite_batch.Draw(number_2, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_3":
                    sprite_batch.Draw(number_3, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_4":
                    sprite_batch.Draw(number_4, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                case "number_5":
                    sprite_batch.Draw(number_5, new Microsoft.Xna.Framework.Rectangle(new Microsoft.Xna.Framework.Point(top_left_coordinate.X, top_left_coordinate.Y), new Microsoft.Xna.Framework.Point((int)width, (int)height)), Convert_color(color));
                    break;
                default:
                    throw new Exception(sprite_name);
            }
        }
    }
}