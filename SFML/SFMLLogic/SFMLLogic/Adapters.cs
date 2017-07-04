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
        Texture pixel = new Texture("white_pixel.png");

        public SFMLDrawingAdapter(RenderWindow window)
        {
            this.window = window;
        }

        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color)
        {
            RectangleShape shape = new RectangleShape(new Vector2f(width, height));
            shape.Position = new Vector2f(top_left_coordinate.X, top_left_coordinate.Y);
            shape.FillColor = Color.Magenta;
            this.window.Draw(shape);
        }
    }
}
