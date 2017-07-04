//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using SFML.System;
//using SFML.Graphics;
//using SFML.Audio;
//using SFML.Window;

//using SharedLogic;


//namespace SFMLLogic

//{
//    /// <summary>
//    /// This is the main type for your game.
//    /// </summary>
//    public class Game1
//    {
//        RenderWindow window = new RenderWindow(new VideoMode(1195, 767), "Main");
//        Texture pixel = new Texture(@"white_pixel.png");
        
//        public const int ScreenWidth = 1195;
//        public const int ScreenHeight = 767;
//        public string device = "Windows";

//        public Game1()
//        {
//            RenderWindow window = new RenderWindow(new VideoMode(1195, 767), "Main");
//            Texture pixel = new Texture("white_pixel.png");
           
//        }

//        EntityManager EntityManager;
//        InputManager InputManager;
//        IDrawingManager IDrawingManager;
//        IDrawVisitor IDrawVisitor;
//        IUpdateVisitor IUpdateVisitor;
//        /// <summary>
//        /// Allows the game to perform any initialization it needs to before starting to run.
//        /// This is where it can query for any required services and load any non-graphic
//        /// related content.  Calling base.Initialize will enumerate through any components
//        /// and initialize them as well.
//        /// </summary>
//        public void Initialize()
//        {
//            //TODO: Add your initialization logic here

//            EntityConstructor entityConstructor = new EntityConstructor();
//            EntityManager = entityConstructor.Instantiate("1", () => window.Close());
//            InputManager = new MouseClick();

//            IUpdateVisitor = new DefaultUpdateVisitor(InputManager, entityConstructor);

//            window.SetActive();
//            while (window.IsOpen)
//            {
//                window.Clear();
//                window.DispatchEvents();
//                Draw();
//                window.Display();
//            }

//        }

//        /// <summary>
//        /// LoadContent will be called once per game and is the place to load
//        /// all of your content.
//        /// </summary>
//        public void LoadContent()
//        {
//            // Create a new SpriteBatch, which can be used to draw textures.
            
//            IDrawingManager = new MonogameDrawingAdapter();
//            IDrawVisitor = new DefaultDrawVisitor(IDrawingManager);

//            // TODO: use this.Content to load your game content here
//        }

//        /// <summary>
//        /// Allows the game to run logic such as updating the world,
//        /// checking for collisions, gathering input, and playing audio.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>
//        //protected override void Update(GameTime gameTime)
//        //{

//        //    EntityManager.Update(IUpdateVisitor, (float)gameTime.ElapsedGameTime.TotalMilliseconds);

//        //    base.Update(gameTime);
//        //}

//        /// <summary>
//        /// This is called when the game should draw itself.
//        /// </summary>
//        /// <param name="gameTime">Provides a snapshot of timing values.</param>
//        public void Draw()
//        {
//            EntityManager EntityManager = new EntityManager();
//            window.Clear(Color.Black);
//            window.DispatchEvents();
//            EntityManager.Draw(IDrawVisitor);
//            Sprite sprite = new Sprite(pixel);
           
//            window.Draw(sprite);



//            // TODO: Add your drawing code here

//        }
//    }

//    public class MonogameDrawingAdapter : IDrawingManager
//    {
//        RectangleShape shape = new RectangleShape();

//        public MonogameDrawingAdapter()
//        {
         
//        }

//        public void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color)
//        {
            
//        }
//    }
//}

