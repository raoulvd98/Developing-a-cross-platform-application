using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gestures;

using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using SharedLogic;

namespace MonoGameLogic
{
    /// <summary>
    /// Registers if the screen is touched and where this is done.
    /// </summary>

    public class MonogameMouseClick : InputManager
    {
        public SharedLogic.Point touchXY;
        public int touchX, touchY;
        public IOption<SharedLogic.Point> Click()
        {
            var mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {

                touchX = Convert.ToInt32(mouse.Position.X);
                touchY = Convert.ToInt32(mouse.Position.Y);
                touchXY = new SharedLogic.Point(touchX, touchY);
                Console.WriteLine(touchX + "   " + touchY);
                return new Some<SharedLogic.Point>(touchXY);
            }
            return new None<SharedLogic.Point>();
        }
    }

    /// <summary>
    /// Enables the InputManager to communicate with Monogame.
    /// </summary>
    public class MonogameTouch : InputManager
    {
        private MonogameMouseClick monogametouch = new MonogameMouseClick();
        public SharedLogic.Point touchXY;
        public int touchX, touchY;

        public IOption<SharedLogic.Point> Click()
        {
            TouchCollection touch = TouchPanel.GetState();

            if (touch.Count > 0)
            {
                if (touch[0].State == TouchLocationState.Moved || touch[0].State == TouchLocationState.Pressed)
                {
                    int touchX = Convert.ToInt32(touch[0].Position.X);
                    int touchY = Convert.ToInt32(touch[0].Position.Y);
                    touchXY = new SharedLogic.Point(touchX, touchY);
                    return new Some<SharedLogic.Point>(touchXY);
                }
            }
            return new None<SharedLogic.Point>();

        }
    }
}