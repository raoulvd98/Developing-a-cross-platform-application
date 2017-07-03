using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project4
{
    /// <summary>
    /// This is the main type of our game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const int ScreenWidth = 1195;
        public const int ScreenHeight = 767;
        public string device = "Windows";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        EntityManager EntityManager;
        InputManager InputManager;
        IDrawingManager IDrawingManager;
        IDrawVisitor IDrawVisitor;
        IUpdateVisitor IUpdateVisitor;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            EntityConstructor entityConstructor = new EntityConstructor();
            EntityManager = entityConstructor.Instantiate("1", () => Exit());
            if (device == "Windows")
                InputManager = new MonogameMouseClick();
            else if (device == "Android")
                InputManager = new MonogameTouch();
            IUpdateVisitor = new DefaultUpdateVisitor(InputManager, entityConstructor);
            this.IsMouseVisible = true;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IDrawingManager = new MonogameDrawingAdapter(spriteBatch, Content);
            IDrawVisitor = new DefaultDrawVisitor(IDrawingManager);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            EntityManager.Update(IUpdateVisitor, (float)gameTime.ElapsedGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            EntityManager.Draw(IDrawVisitor);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}