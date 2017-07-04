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
        Sprite sprite;
        Texture pixel = new Texture("white_pixel.png");
        Texture n0 = new Texture("number_0.png");
        Texture n1 = new Texture("number_1.png");
        Texture n2 = new Texture("number_2.png");
        Texture n3 = new Texture("number_3.png");
        Texture n4 = new Texture("number_4.png");
        Texture n5 = new Texture("number_5.png");


        public SFMLDrawingAdapter(RenderWindow window)
        {
            this.window = window;
        }

        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color, string name)
        {


            switch (name)
            {
                case "white_pixel":
                    sprite = new Sprite(pixel);
                    //sprite.Scale = new Vector2f(width, height);
                    ////RectangleShape shape = new RectangleShape(new Vector2f(width, height));
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    
                    break;
                case "number_0":
                    sprite = new Sprite(n0);
                    //sprite.Scale = new Vector2f(width, height);
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    break;
                case "number_1":
                    sprite = new Sprite(n1);
                    //sprite.Scale = new Vector2f(width, height);
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    break;
                case "number_2":
                    sprite = new Sprite(n2);
                    //sprite.Scale = new Vector2f(width, height);
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    break;
                case "number_3":
                    sprite = new Sprite(n3);
                    //sprite.Scale = new Vector2f(width, height);
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    break;
                case "number_4":
                    sprite = new Sprite(n4);
                    //sprite.Scale = new Vector2f(width, height);
                    //sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
                    //sprite.Color = Color.Magenta;
                    //this.window.Draw(sprite);
                    break;
                case "number_5":
                    sprite = new Sprite(n5);

                    break;
                default:

                    throw new Exception(name);
            }
            sprite.Scale = new Vector2f(width, height);
            sprite.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
            sprite.Color = Color.Magenta;
            this.window.Draw(sprite);
        }
    }
}
