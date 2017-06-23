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
    /// <summary>
    /// Factory to allow for the creation of entities.
    /// The ball receives a random direction upon spawn.
    /// </summary>
    public interface IEntityFactory
    {
        Entity Create(string Entityname);
    }
    class EntityFactory : IEntityFactory
    {
        public Entity Create(string Entityname)
        {
            switch (Entityname)
            {
                case "Ball":
                    return new Ball(new Vector2(RandomNumber(), RandomNumber()), new Rectangle(560, 378, 30, 30));
                case "PaddleLeft":
                    return new Paddle(new Vector2(0), new Rectangle(30, 334, 30, 100));
                case "PaddleRight":
                    return new Paddle(new Vector2(0), new Rectangle(1150, 334, 30, 100));
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

    /// <summary>
    /// Definition of all entities.
    /// </summary>
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
            visitor.DrawEntity(this);
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

    /// <summary>
    /// Allows visitor to visit entities.
    /// </summary>
    public class EntityManager : IDrawUpdate
    {
        public List<Entity> entities;

        public void Draw(IDrawVisitor visitor)
        {
            visitor.DrawScreen(this);
        }

        public void Update(IUpdateVisitor visitor, float dt)
        {
            visitor.UpdateEntity(this, dt);
        }

    }

    /// <summary>
    /// Adds entities to a list.
    /// </summary>
    public class EntityConstructor
    {
        public EntityManager Instantiate(string option, Action exit)
        {
            EntityManager entityManager = new EntityManager();
            switch (option)
            {
                default:
                    {
                        EntityFactory entityCreator = new EntityFactory();
                        Entity Ball = entityCreator.Create("Ball");
                        Entity PaddleLeft = entityCreator.Create("PaddleLeft");
                        Entity PaddleRight = entityCreator.Create("PaddleRight");
                        entityManager.entities = new List<Entity>();
                        entityManager.entities.Add(Ball);
                        entityManager.entities.Add(PaddleLeft);
                        entityManager.entities.Add(PaddleRight);

                        break;
                    }
            }
            return entityManager;
        }
    }
}