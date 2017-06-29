using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Threading;

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
                    return new Ball(new Vector2(RandomNumber(), RandomNumber()), new Vector2(560, 378), 30, 30, "Ball", 0);
                case "PaddleLeft":
                    return new Paddle(new Vector2(0), new Vector2(0, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight, "PaddleLeft", 0);
                case "PaddleRight":
                    return new Paddle(new Vector2(0), new Vector2(0.961f * Game1.ScreenWidth, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight, "PaddleRight", 0);
                case "BorderLineTop":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineTop", 0);
                case "BorderLineBottom":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineBottom", 0);
                case "MiddleLine":
                    return new BorderLine(new Vector2(0), new Vector2(0.496f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.004f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight, "MiddleLine", 0);
            }
            throw new Exception("Entity creation failed");
        }

        public static float RandomNumber()
        {
            // Create a random number to determine the velocity
            Random random = new Random();
            int number = random.Next(0, 1);

            switch (number)
            {
                case 1:
                    return 0.2f;
                case 0:
                    return 0.2f;
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
        public int score;

        public Entity(Vector2 velocity, Vector2 Position, float width, float height, string name, int score)
        {
            this.Velocity = velocity;
            this.Position = Position;
            this.width = width;
            this.height = height;
            this.name = name;
            this.score = score;
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
        public virtual void ChangeVelocity(InputManager input_manager, float dt, Entity Ball)
        {
            Position.X = Position.X + Velocity.X * dt;
            Position.Y = Position.Y + Velocity.Y * dt;
        }
        public virtual void AddScore(Entity paddle) { }
        public virtual void CheckOutOfBounds(Entity PaddleLeft, Entity PaddleRight)
        {

        }
    }
    public class Ball : Entity
    {
        public Ball(Vector2 velocity, Vector2 Position, float width, float height, string name, int score) : base(velocity,Position, width, height, name, score){}

        public override void Checkcollision(Entity PaddleLeft, Entity PaddleRight)
        {
            //Border collision
            if ((Position.Y + 30) >= (0.948f * Game1.ScreenHeight) || (Position.Y - 30) <= (0.013f * Game1.ScreenHeight))
            {
                float Y = Velocity.Y;
                float X = Velocity.X;
                Y = Y * -1;
                Velocity = new Vector2(X, Y);
            }

            //Paddle collision
            if (((Position.X <= (PaddleLeft.Position.X + PaddleLeft.width)) && ((Position.Y + height) >= PaddleLeft.Position.Y) && (Position.Y <= (PaddleLeft.Position.Y + PaddleLeft.height)))
                || (((Position.X + width) >= PaddleRight.Position.X) && ((Position.Y + height) >= PaddleRight.Position.Y) && (Position.Y <= (PaddleRight.Position.Y + PaddleRight.height))))
            {
                float Y = Velocity.Y;
                float X = Velocity.X;
                X = X * -1;
                Velocity = new Vector2(X, Y);
            }
        }

        public override void CheckOutOfBounds(Entity PaddleLeft, Entity PaddleRight)
        {
            if (Position.X <= -30) { AddScore(PaddleRight); }
            if (Position.X >= Game1.ScreenWidth) { AddScore(PaddleLeft); }
            if ((Position.X <= -30) || (Position.X >= Game1.ScreenWidth))
            {
                Position.X = 560;
                Position.Y = 378;
                Velocity = new Vector2(0, 0);
                Velocity = new Vector2(EntityFactory.RandomNumber(), EntityFactory.RandomNumber());
                PaddleLeft.Position.Y = 0.435f * Game1.ScreenHeight;
                PaddleRight.Position.Y = 0.435f * Game1.ScreenHeight;
            }
            Console.WriteLine(PaddleLeft.score + "  " + PaddleRight.score);
        }

        public override void ChangeVelocity(InputManager input_manager, float dt, Entity Ball)
        {
            base.ChangeVelocity(input_manager, dt, Ball);
        }

        public override void AddScore(Entity paddle)
        {
            base.AddScore(paddle);
            paddle.score += 1;
        }
    }

    public class Paddle : Entity
    {
        public Paddle(Vector2 velocity, Vector2 Position, float width, float height, string name, int score) : base(velocity, Position, width, height, name, score) { }
        public override void ChangeVelocity(InputManager input_manager, float dt, Entity Ball)
        {
            base.ChangeVelocity(input_manager, dt, Ball);
            
            switch (name)
            {
                case "PaddleLeft":
                    Velocity.Y = Ball.Velocity.Y * 1f;
                    break;
                case "PaddleRight":
                    input_manager.Touch().Visit(() => Velocity.Y = 0,
                                                xy => MousePosition(xy));
                    break;
            }
        }

        public float MousePosition(Point touchXY)
        {
            if (touchXY.Y > (Position.Y + height)) {Velocity.Y = 0.30f; }
            else if (touchXY.Y < Position.Y) { Velocity.Y = -0.30f; }
            else { Velocity.Y = 0f; }
            return Velocity.Y;
        }
    }
    public class BorderLine : Entity
    {
        public BorderLine(Vector2 velocity, Vector2 Position, float width, float height, string name, int score) : base(velocity, Position, width, height, name, score) { }
        public override void Checkcollision(Entity PaddleLeft, Entity PaddleRight)
        {
            switch (name)
            {
                case "BorderLineTop":
                    if ((PaddleRight.Position.Y) <= Position.Y + height) { PaddleRight.Position.Y = (Position.Y + height); }
                    if ((PaddleLeft.Position.Y) <= Position.Y + height) { PaddleLeft.Position.Y = (Position.Y + height); }
                    break;
                case "BorderLineBottom":
                    if ((PaddleRight.Position.Y + PaddleRight.height) >= Position.Y) { PaddleRight.Position.Y = (Position.Y - PaddleLeft.height); }
                    if ((PaddleLeft.Position.Y + PaddleLeft.height) >= Position.Y) { PaddleLeft.Position.Y = (Position.Y - PaddleLeft.height); }
                    break;
            }
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