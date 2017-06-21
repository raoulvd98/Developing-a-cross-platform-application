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

namespace Project4
{
    public interface DrawingManager
    {
        void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color);
        void DrawString(string text, Point top_left_coordinate, int size, Colour color);
    }

    public enum Colour { White, Black, Blue };

    public class MonogameDrawingAdapter : DrawingManager
    {
        SpriteBatch sprite_batch;
        ContentManager content_manager;

        Texture2D white_pixel;
        SpriteFont default_font;
        Game game;

        public MonogameDrawingAdapter(SpriteBatch sprite_batch, ContentManager content_manager)
        {
            this.sprite_batch = sprite_batch;
            this.content_manager = content_manager;
            white_pixel = content_manager.Load<Texture2D>("white_pixel");
            default_font = content_manager.Load<SpriteFont>("arial");
        }

        private Microsoft.Xna.Framework.Color convert_color(Colour color)
        {
            switch (color)
            {
                case Colour.Black:
                    return Microsoft.Xna.Framework.Color.Black;
                case Colour.White:
                    return Microsoft.Xna.Framework.Color.White;
                case Colour.Blue:
                    return Microsoft.Xna.Framework.Color.Blue;
                default:
                    return Microsoft.Xna.Framework.Color.White;
            }
        }

        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color)
        {
            sprite_batch.Draw(white_pixel, new Rectangle((int)top_left_coordinate.X, (int)top_left_coordinate.Y, (int)width, (int)height), convert_color(color));
        }

        public void DrawString(string text, Point top_left_coordinate, int size, Colour color)
        {
            sprite_batch.DrawString(default_font, text, new Vector2(top_left_coordinate.X, top_left_coordinate.Y), convert_color(color));
        }
    }
}