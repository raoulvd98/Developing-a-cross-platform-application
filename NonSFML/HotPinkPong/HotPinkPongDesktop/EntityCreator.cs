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
                    return new Ball(new Vector2(RandomNumber(), RandomNumber()), new Vector2(560, 378), 30, 30, "Ball", 0, "white_pixel");
                case "PaddleLeft":
                    return new Paddle(new Vector2(0), new Vector2(0, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight, "PaddleLeft", 0, "white_pixel");
                case "PaddleRight":
                    return new Paddle(new Vector2(0), new Vector2(0.961f * Game1.ScreenWidth, 0.435f * Game1.ScreenHeight), 0.025f * Game1.ScreenWidth, 0.130f * Game1.ScreenHeight, "PaddleRight", 0, "white_pixel");
                case "BorderLineTop":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineTop", 0, "white_pixel");
                case "BorderLineBottom":
                    return new BorderLine(new Vector2(0), new Vector2(0.025f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight), 0.950f * Game1.ScreenWidth, 0.039f * Game1.ScreenHeight, "BorderLineBottom", 0, "white_pixel");
                case "MiddleLine":
                    return new BorderLine(new Vector2(0), new Vector2(0.496f * Game1.ScreenWidth, 0.013f * Game1.ScreenHeight), 0.004f * Game1.ScreenWidth, 0.948f * Game1.ScreenHeight, "MiddleLine", 0, "white_pixel");
                case "ScoreLeft":
                    return new LeftScore(new Vector2(0), new Vector2(0.450f * Game1.ScreenWidth, 0.060f * Game1.ScreenHeight), 0.040f * Game1.ScreenWidth, 0.1f * Game1.ScreenHeight, "Score", 0, "number_0");
                case "ScoreRight":
                    return new RightScore(new Vector2(0), new Vector2(0.506f * Game1.ScreenWidth, 0.060f * Game1.ScreenHeight), 0.040f * Game1.ScreenWidth, 0.1f * Game1.ScreenHeight, "Score", 0, "number_0");
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
        public string sprite_name;

        public Entity(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name)
        {
            this.Velocity = velocity;
            this.Position = Position;
            this.width = width;
            this.height = height;
            this.name = name;
            this.score = score;
            this.sprite_name = sprite_name;
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
        public virtual void AddScore() { }
        public virtual void CheckOutOfBounds(Entity PaddleLeft, Entity PaddleRight, Entity LeftScore, Entity Rightscore) { }
        public virtual void Change_SpriteName() { }
    }
    public class Ball : Entity
    {
        public Ball(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name) : base(velocity, Position, width, height, name, score, sprite_name) { }

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
            
        public override void CheckOutOfBounds(Entity PaddleLeft, Entity PaddleRight, Entity LeftScore, Entity Rightscore)
        {
            if (Position.X <= -30) { Rightscore.AddScore();  }
            if (Position.X >= Game1.ScreenWidth) { LeftScore.AddScore(); }
            if ((Position.X <= -30) || (Position.X >= Game1.ScreenWidth))
            {
                Position.X = 560;
                Position.Y = 378;
                Velocity = new Vector2(0, 0);
                Velocity = new Vector2(EntityFactory.RandomNumber(), EntityFactory.RandomNumber());
                PaddleLeft.Position.Y = 0.435f * Game1.ScreenHeight;
                PaddleRight.Position.Y = 0.435f * Game1.ScreenHeight;
            }
        }

        public override void ChangeVelocity(InputManager input_manager, float dt, Entity Ball)
        {
            base.ChangeVelocity(input_manager, dt, Ball);
        }


    }
    public class Paddle : Entity
    {
        public Paddle(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name) : base(velocity, Position, width, height, name, score, sprite_name) { }
        public override void ChangeVelocity(InputManager input_manager, float dt, Entity Ball)
        {
            base.ChangeVelocity(input_manager, dt, Ball);

            switch (name)
            {
                case "PaddleLeft":
                    Velocity.Y = Ball.Velocity.Y * 0.85f;
                    break;
                case "PaddleRight":
                    input_manager.Touch().Visit(() => Velocity.Y = 0,
                                                xy => MousePosition(xy));
                    break;
            }
        }

        public float MousePosition(Point touchXY)
        {
            if (touchXY.Y > (Position.Y + height)) { Velocity.Y = 0.30f; }
            else if (touchXY.Y < Position.Y) { Velocity.Y = -0.30f; }
            else { Velocity.Y = 0f; }
            return Velocity.Y;
        }
    }
    public class BorderLine : Entity
    {
        public BorderLine(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name) : base(velocity, Position, width, height, name, score, sprite_name) { }
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

    public class LeftScore : Entity
    {
        public LeftScore(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name) : base(velocity, Position, width, height, name, score, sprite_name) { }

        public override void AddScore()
        {
            score += 1;
            Change_SpriteName();
        }
        public override void Change_SpriteName()
        {
            base.Change_SpriteName();
            switch (score)
            {
                case 1:
                    sprite_name = "number_1";
                    break;
                case 2:
                    sprite_name = "number_2";
                    break;
                case 3:
                    sprite_name = "number_3";
                    break;
                case 4:
                    sprite_name = "number_4";
                    break;
                case 5:
                    sprite_name = "number_5";
                    break;
            }
        }
    }
    public class RightScore : Entity
    {
        public RightScore(Vector2 velocity, Vector2 Position, float width, float height, string name, int score, string sprite_name) : base(velocity, Position, width, height, name, score, sprite_name) { }
        public override void AddScore()
        {
            score += 1;
            Change_SpriteName();
        }
        public override void Change_SpriteName()
        {
            base.Change_SpriteName();
            switch (score)
            {
                case 1:
                    sprite_name = "number_1";
                    break;
                case 2:
                    sprite_name = "number_2";
                    break;
                case 3:
                    sprite_name = "number_3";
                    break;
                case 4:
                    sprite_name = "number_4";
                    break;
                case 5:
                    sprite_name = "number_5";
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
        public Entity ScoreLeft;
        public Entity ScoreRight;

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
                        ScoreLeft = entityCreator.Create("ScoreLeft");
                        ScoreRight = entityCreator.Create("ScoreRight");

                        entityManager.entities = new List<Entity>();
                        entityManager.entities.Add(Ball);
                        entityManager.entities.Add(PaddleLeft);
                        entityManager.entities.Add(PaddleRight);
                        entityManager.entities.Add(BorderLineTop);
                        entityManager.entities.Add(BorderLineBottom);
                        entityManager.entities.Add(MiddleLine);
                        entityManager.entities.Add(ScoreLeft);
                        entityManager.entities.Add(ScoreRight);

                        break;
                    }
            }
            return entityManager;
        }
    }
}