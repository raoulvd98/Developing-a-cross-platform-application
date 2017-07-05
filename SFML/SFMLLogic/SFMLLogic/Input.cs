using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedLogic;

using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;



namespace SFMLLogic
{
    /// <summary>
    /// Registers if the screen is clicked and where on the screen this is done.
    /// </summary>


    public class MouseClick : InputManager
    {
        public Point clickXY;
        public int clickX, clickY;
        public IOption<Point> Click()
        {
            var mousePosition = Mouse.GetPosition();
            var mouseState = Mouse.IsButtonPressed(Mouse.Button.Left);

            if (mouseState)
            {

                clickX = Convert.ToInt32(mousePosition.X);
                clickY = Convert.ToInt32(mousePosition.Y);
                clickXY = new Point(clickX, clickY);
                return new Some<Point>(clickXY);
            }
            return new None<Point>();
        }
    }
}
