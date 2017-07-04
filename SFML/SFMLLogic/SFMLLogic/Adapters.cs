using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;

using SharedLogic;

namespace SFMLLogic
{
    public struct Vector2
    {
        float x, y;
        Vector2f vector2;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.vector2 = new Vector2f(x, y);
        }

        public static implicit operator Vector2f(Vector2 v)
        {
            return new Vector2f(v.x, v.y);
        }
    }
    public class SFMLDrawingAdapter : IDrawingManager
    {
        RenderWindow window;
        Sprite sprite = new Sprite();
        Texture pixel = new Texture("white_pixel.png");


        public SFMLDrawingAdapter(RenderWindow window)
        {
            this.window = window;
        }

        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color, string name)
        {


            switch (name)
            {
                case "white_pixel":
                    sprite.Texture = pixel;
                    sprite.Scale = new Vector2f(width, height);
                    sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    sprite.Color = Color.Magenta;
                    this.window.Draw(sprite);
                    break;
                case "number_0":
                    break;
                case "number_1":
                    sprite.Texture = pixel;
                    for (int i = 0; i < 1; i++)
                    {
                        sprite.Scale = new Vector2f(width / 4, height);
                        sprite.Position = new Vector2f(top_left_coordinate.X - (i*30), top_left_coordinate.Y);
                        sprite.Color = Color.Magenta;
                        this.window.Draw(sprite);
                    }
                    break;
                case "number_2":
                    for (int i = 0; i < 2; i++)
                    {
                        sprite.Scale = new Vector2f(width / 4, height);
                        sprite.Position = new Vector2f(top_left_coordinate.X - (i * 30), top_left_coordinate.Y);
                        sprite.Color = Color.Magenta;
                        this.window.Draw(sprite);
                    }
                    break;
                case "number_3":
                    for (int i = 0; i < 3; i++)
                    {
                        sprite.Scale = new Vector2f(width / 4, height);
                        sprite.Position = new Vector2f(top_left_coordinate.X - (i * 30), top_left_coordinate.Y);
                        sprite.Color = Color.Magenta;
                        this.window.Draw(sprite);
                    }
                    break;
                case "number_4":
                    for (int i = 0; i < 4; i++)
                    {
                        sprite.Scale = new Vector2f(width / 4, height);
                        sprite.Position = new Vector2f(top_left_coordinate.X - (i * 30), top_left_coordinate.Y);
                        sprite.Color = Color.Magenta;
                        this.window.Draw(sprite);
                    }
                    break;
                case "number_5":
                    for (int i = 0; i < 4; i++)
                    {
                        sprite.Scale = new Vector2f(width / 4, height);
                        sprite.Position = new Vector2f(top_left_coordinate.X - (i * 30), top_left_coordinate.Y);
                        sprite.Color = Color.Magenta;
                        this.window.Draw(sprite);
                    }
                    sprite.Scale = new Vector2f(width / 4, height+70);
                    sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y+20);
                    sprite.Rotation = 90;
                    sprite.Color = Color.Magenta;
                    this.window.Draw(sprite);
                    sprite.Rotation = 0;
                    break;
                default:

                    throw new Exception(name);
            }
        }
    }
}
