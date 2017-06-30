using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Graphics;
using SFML.Window;

namespace SFML_TEST
{
    class ProgramSFML
    {
        static void Main(string[] args)
        {


            RenderWindow window = new RenderWindow(new VideoMode(200, 200), "test");
            CircleShape cs = new CircleShape(100.0f);
            cs.FillColor = Color.Magenta;
            window.SetActive();
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                window.Draw(cs);
                window.Display();
            }


        }

    }
}
