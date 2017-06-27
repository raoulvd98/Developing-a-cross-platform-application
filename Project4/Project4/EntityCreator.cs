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
                    return new Ball(new Vector2(0.2f,0), new Vector2(560, 378), 30, 30,"Ball");
                case "PaddleLeft":
                    return new Paddle(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight,"Left");
                case "PaddleRight":
                    return new Paddle(new Vector2(0), new Vector2(0.950f * Game1.ScreenWidth, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight, "Right");
                case "BorderLineTop":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineTop");
                case "BorderLineBottom":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineBottom");
                case "MiddleLine":
                    return new BorderLine(new Vector2(0), new Vector2(0.496f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.004f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight, "MiddleLine");
            }
            throw new Exception("Entity creation failed");
        }

        public float RandomNumber()
        {
            // Create a random number to determine the velocity
            Random random = new Random();
            int number = random.Next(-1, 1);

            switch (number)
            {
                case 1:
                    return 0.1f;
                case -1:
                    return 0.1f;
            }
            return RandomNumber();
        }
    }

    /// <summary>
    /// Definition of all entities.
    /// </summary>
    public abstract class Entity
    {
        public Vector2 Velocity;
        public Vector2 Position;
        public float width;
        public float height;
        public string name;

        public Entity(Vector2 velocity, Vector2 Position, float width, float height, string name)
        {
            this.Velocity = velocity;
            this.Position = Position;
            this.width = width;
            this.height = height;
            this.name = name;
        }

        public void Draw(IDrawVisitor visitor)
        {
            visitor.DrawEntity(this);
        }

        public void Update(IUpdateVisitor visitor, float dt)
        {
            visitor.UpdateEntity(this, dt);
        }

        public virtual void Checkcollision(Entity PaddleLeft, Entity PaddleRight) { }
        public virtual void ChangeVelocity(MonogameTouch input_manager, float dt)
        {
            Position.X = Position.X + Velocity.X * dt;
            Position.Y = Position.Y + Velocity.Y * dt;
        }
    }
    public class Ball : Entity
    {
        public Ball(Vector2 velocity, Vector2 Position, float width, float height, string name) : base(velocity,Position, width, height, name){}

        public override void Checkcollision(Entity PaddleLeft, Entity PaddleRight)
        {
            if ((Position.Y + 30) >= (0.948f * Game1.ScreenHeight) || (Position.Y - 30) <= (0.013f * Game1.ScreenHeight))
            {
                float Y = Velocity.Y;
                float X = Velocity.X;
                Y = Y * -1;
                Velocity = new Vector2(X, Y);
            }
            
            if (((Position.X) <= (PaddleLeft.Position.X + PaddleLeft.width) && (Position.Y + height) >= (PaddleLeft.Position.Y) && (Position.Y) <= (PaddleLeft.Position.Y + PaddleLeft.height))
                || ((Position.X + width) >= (PaddleRight.Position.X) && (Position.Y + height) >= (PaddleRight.Position.Y) && (Position.Y) <= (PaddleRight.Position.Y + PaddleRight.height)))
            {
                    float Y = Velocity.Y;
                    float X = Velocity.X;
                    X = X * -1;
                    Velocity = new Vector2(X, Y);
            }
        }

        public override void ChangeVelocity(MonogameTouch input_manager, float dt)
        {
            base.ChangeVelocity(input_manager, dt);
        }
    }
    public class Paddle : Entity
    {
        public Paddle(Vector2 velocity, Vector2 Position, float width, float height, string name) : base(velocity, Position, width, height, name){}
        public override void ChangeVelocity(MonogameTouch input_manager, float dt)
        {
            base.ChangeVelocity(input_manager, dt);
            input_manager.Touch().Visit(
                () => Velocity.Y = 0,
                _ => MousePosition(input_manager.touchXY));
           // input_manager.Touch().Visit(() => Velocity.Y = 0.0f, _ => Velocity.Y = 0.30f);
        }
        public float MousePosition(Point touchXY)
        {
            if ( touchXY.Y >= (Game1.ScreenHeight / 2)) { Velocity.Y = 0.30f; }
            else { Velocity.Y = -0.30f; }
            Console.WriteLine(touchXY.Y);
            return Velocity.Y;
        }
    }
    public class BorderLine : Entity
    {
        public BorderLine(Vector2 velocity, Vector2 Position, float width, float height, string name) : base(velocity, Position, width, height, name){}
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
            visitor.UpdateScreen(this, dt);
        }
    }

    /// <summary>
    /// Adds entities to a list.
    /// </summary>
    public class EntityConstructor
    {
        public Entity Ball;
        public Entity PaddleLeft;
        public Entity PaddleRight;

        public EntityManager Instantiate(string option, Action exit)
        {
            EntityManager entityManager = new EntityManager();
            switch (option)
            {
                default:
                    {
                        EntityFactory entityCreator = new EntityFactory();
                        Ball = entityCreator.Create("Ball");
                        PaddleLeft = entityCreator.Create("PaddleLeft");
                        PaddleRight = entityCreator.Create("PaddleRight");
                        Entity BorderLineTop = entityCreator.Create("BorderLineTop");
                        Entity BorderLineBottom = entityCreator.Create("BorderLineBottom");
                        Entity MiddleLine = entityCreator.Create("MiddleLine");

                        entityManager.entities = new List<Entity>();
                        entityManager.entities.Add(Ball);
                        entityManager.entities.Add(PaddleLeft);
                        entityManager.entities.Add(PaddleRight);
                        entityManager.entities.Add(BorderLineTop);
                        entityManager.entities.Add(BorderLineBottom);
                        entityManager.entities.Add(MiddleLine);

                        break;
                    }
            }
            return entityManager;
        }
    }
}