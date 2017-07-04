using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharedLogic
{
    /// <summary>
    /// Implements the IDrawable and IUpdatable for easy access.
    /// </summary>
    public interface IDrawUpdate : IDrawable, IUpdatable { }

    /// <summary>
    /// Define interfaces to allow for drawing entities and the screen.
    /// </summary>
    public interface IDrawingManager
    {
        void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color, string sprite_name);
    }
    public interface IDrawable { void Draw(IDrawVisitor visitor); }
    public interface IDrawVisitor
    {
        void DrawScreen(EntityManager entityManager);
        void DrawEntity(Entity entity);
    }
    public enum Colour { White, Hotpink };

    /// <summary>
    /// Define interfaces to allow for updating entities and screen.
    /// </summary>
    public interface IUpdateVisitor
    {
        void UpdateScreen(EntityManager entityManager, float dt);
        void UpdateEntity(Entity entity, float dt);
    }
    public interface IUpdatable { void Update(IUpdateVisitor visitor, float dt); }

    /// <summary>
    /// Concrete implementations for drawing entities on the screen.
    /// </summary>


    /// <summary>
    /// Concrete implementation for visiting the list and drawing.
    /// </summary>
    public class DefaultDrawVisitor : IDrawVisitor
    {
        IDrawingManager drawing_manager;

        public DefaultDrawVisitor(IDrawingManager drawing_manager)
        {
            this.drawing_manager = drawing_manager;
        }

        public void DrawEntity(Entity entity)
        {
            drawing_manager.DrawRectangle(new Point(Convert.ToInt32(entity.Position.X), Convert.ToInt32(entity.Position.Y)), entity.width, entity.height, Colour.Hotpink, entity.sprite_name);
        }

        public void DrawScreen(EntityManager entityManager)
        {
            entityManager.entities.Reset();
            while (entityManager.entities.GetNext().Visit(() => false, _ => true))
            {
                entityManager.entities.GetCurrent().Visit(() => { }, item => { item.Draw(this); });
            }
        }
    }
    public class DefaultUpdateVisitor : IUpdateVisitor
    {
        InputManager input_manager;
        EntityConstructor constructor;

        public DefaultUpdateVisitor(InputManager input_manager, EntityConstructor constructor)
        {
            this.input_manager = input_manager;
            this.constructor = constructor;
        }

        public void UpdateEntity(Entity entity, float dt)
        {
            entity.Checkcollision(constructor.PaddleLeft, constructor.PaddleRight);
            entity.ChangeVelocity(input_manager, dt, constructor.Ball);
            entity.CheckOutOfBounds(constructor.PaddleLeft, constructor.PaddleRight, constructor.ScoreLeft, constructor.ScoreRight);
        }

        public void UpdateScreen(EntityManager entityManager, float dt)
        {
            entityManager.entities.Reset();
            while (entityManager.entities.GetNext().Visit(() => false, _ => true))
            {
                entityManager.entities.GetCurrent().Visit(() => { }, item => { item.Update(this, dt); });
            }
        }
    }
}