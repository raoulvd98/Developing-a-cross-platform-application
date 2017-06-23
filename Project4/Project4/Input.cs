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


namespace Project4
{
    public interface InputManager
    {
        IOption<Point> Touch();
    }

    public class MonogameTouch : InputManager
    { 
        public IOption<Point> Touch()
        {
            TouchCollection touch = TouchPanel.GetState();

            if (touch.Count > 0)
            {
                if (touch[0].State == TouchLocationState.Moved || touch[0].State == TouchLocationState.Pressed)
                {
                    int touchX = Convert.ToInt32(touch[0].Position.X);
                    int touchY = Convert.ToInt32(touch[0].Position.Y);
                    Point touchXY = new Point(touchX, touchY);
                    return new Some<Point>(touchXY);
                }
            }
            return new None<Point>();
        }
    }
}