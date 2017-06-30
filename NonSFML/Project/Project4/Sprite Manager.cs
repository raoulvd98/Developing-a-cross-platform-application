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

namespace Project4
{
    public class GuiManager : Updateable, Drawable
    {
        public List<GuiElement> elements;

        public void Draw(DrawVisitor visitor)
        {
            visitor.DrawGui(this);
        }

        public void Update(UpdateVisitor visitor, float dt)
        {
            visitor.UpdateGui(this, dt);
        }
    }