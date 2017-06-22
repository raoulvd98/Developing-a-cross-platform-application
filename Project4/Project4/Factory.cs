using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Project4
{ 
    public interface IEntityFactory
    {
        Entity Create(int Entitynumber);
    }
 
    public class Entity
    {
        public Vector2 Velocity;
        public Rectangle Rectangle;

        public Entity(Vector2 velocity, Rectangle rectangle)
        {
            this.Velocity = velocity;
            this.Rectangle = rectangle;          
        }

        public void Draw(IDrawVisitor visitor)
        {
            visitor.DrawBall(this);
        }
    }

    public class Ball : Entity
    {
        public Ball(Vector2 velocity, Rectangle rectangle) : base(velocity, rectangle)
        {
            this.Velocity = velocity;
            this.Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

    }

    public class Paddle : Entity
    {
        public Paddle(Vector2 velocity, Rectangle rectangle) : base(velocity, rectangle)
        {
            this.Velocity = velocity;
            this.Rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }

    class EntityFactory : IEntityFactory
    {

        // Method which create Balls en Paddles
        public Entity Create(int Entitynumber)
        {
            switch (Entitynumber)
            {
                case 0:
                    return new Ball(new Vector2(RandomNumber(), RandomNumber()),new Rectangle(50,50,10,10));
                case 1:
                    return new Paddle(new Vector2(0), new Rectangle(10, 50, 10, 50));
                case 2:
                    return new Paddle(new Vector2(0), new Rectangle(-10, 50, 10, 50));
            }
            throw new Exception("Entity creation failed");
        }

        public int RandomNumber()
        {
            // Create a random number to determine the velocity
            Random random = new Random();
            int number = random.Next(-1, 1);

            switch (number)
            {
                case 1:
                    return 10;
                case -1:
                    return -10;
            }
            return RandomNumber();
        }
    }
}