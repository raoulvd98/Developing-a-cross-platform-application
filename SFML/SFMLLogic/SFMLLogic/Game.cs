using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;

using SharedLogic;

namespace SFMLLogic
{
    /// <summary>
    /// Run the SFMLLogic, drawing and updating the game.
    /// </summary>
    class Game
    {
        RenderWindow window = new RenderWindow(new VideoMode(1195, 767), "test");
        Texture pixel = new Texture("white_pixel.png");
        Sprite sprite;

        EntityManager EntityManager;
        InputManager InputManager;
        IDrawingManager IDrawingManager;
        IDrawVisitor IDrawVisitor;
        IUpdateVisitor IUpdateVisitor;

        public void Run()
        {
            sprite = new Sprite(pixel);

            EntityConstructor entityConstructor = new EntityConstructor();
            EntityManager = entityConstructor.Instantiate("1", () => window.Close());
            InputManager = new MouseClick();
            IUpdateVisitor = new DefaultUpdateVisitor(InputManager, entityConstructor);


            IDrawingManager = new SFMLDrawingAdapter(window);
            IDrawVisitor = new DefaultDrawVisitor(IDrawingManager);
            
            //Database.Set_connection();
            //Database.ExecuteData("create table if exists WinsLosses (ID INTEGER PRIMARY KEY, Wins INTEGER, Losses INTEGER)");
            //Database.ExecuteData("insert into WinsLosses (ID, Wins, Losses) values (1,0,0)");
            //Database.ReadData("select * from WinsLosses");

            window.SetActive();

            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                Draw();
                Update();
                window.Display();
            }
        }

        public void Draw()
        {
            EntityManager.Draw(IDrawVisitor);
        }

        public void Update()
        {
            EntityManager.Update(IUpdateVisitor, 4);
        }
    }
}
