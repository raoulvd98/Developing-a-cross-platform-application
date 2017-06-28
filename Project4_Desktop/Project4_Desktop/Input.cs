using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Project4
{
    /// <summary>
    /// The InputManager registers if the screen is touched.
    /// </summary>
    public interface InputManager
    {
        IOption<Point> Touch();
    }

    public class MonogameMouseClick
    {
        //public IOption<Point> Touch(Point touchXY, int touchX, int touchY)
        //{
        //    MouseState state = Mouse.GetState();
        //    var mousePosition = new Point(state.X, state.Y);

        //    if (state.LeftButton == ButtonState.Pressed)
        //    {
        //        //if (touch[0].State == TouchLocationState.Moved || touch[0].State == TouchLocationState.Pressed)
        //        if (touch[0].State == TouchLocationState.Moved || touch[0].State == TouchLocationState.Pressed)
        //        {
        //            touchX = Convert.ToInt32(touch[0].Position.X);
        //            touchY = Convert.ToInt32(touch[0].Position.Y);
        //            touchXY = new Point(touchX, touchY);
        //            return new Some<Point>(touchXY);
        //        }
        //    }
        //    return new None<Point>();
        //}
    }

    /// <summary>
    /// Enables the InputManager to communicate with Monogame.
    /// </summary>
    public class MonogameTouch : InputManager
    {
        private MonogameMouseClick monogametouch;
        public Point touchXY;
        public int touchX, touchY;

        //public MonogameTouch(Point touchXY, int touchX, int touchY)
        //{
        //    this.touchX = touchX;
        //    this.touchY = touchY;
        //    this.touchXY = touchXY;
        //    this.monogametouch = new MonogameMouseClick();
        //}

        public IOption<Point> Touch()
        {
            TouchCollection touch = TouchPanel.GetState();

            if (touch.Count > 0)
            {
                if (touch[0].State == TouchLocationState.Moved || touch[0].State == TouchLocationState.Pressed)
                {
                    int touchX = Convert.ToInt32(touch[0].Position.X);
                    int touchY = Convert.ToInt32(touch[0].Position.Y);
                    touchXY = new Point(touchX, touchY);
                    return new Some<Point>(touchXY);
                }
            }
            return new None<Point>();
        }
    }
}