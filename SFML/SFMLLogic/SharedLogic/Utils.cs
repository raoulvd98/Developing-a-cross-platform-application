using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }

    public class Rectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }

    public class Vector2
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(float xy)
        {
            this.X = xy;
            this.Y = xy;
        }

    }
    
}
