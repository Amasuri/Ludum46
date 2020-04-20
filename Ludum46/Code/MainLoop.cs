using Ludum46.Code.Graphics;
using Ludum46.Code.Level;
using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ludum46.Code
{
    public class Ludum46 : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public static float DeltaUpdate { get; private set; }
        public static float DeltaDraw { get; private set; }

        static public int Scale { get; private set; }
        static public int UnscaledWidth { get; private set; }
        static public int UnscaledHeight { get; private set; }

        static public int ScaledWidth => UnscaledWidth * Scale;
        static public int ScaledHeight => UnscaledHeight * Scale;

        public ScreenPool screenPool;
        public ObjectPool pool;
        public Level.Level level;

        public MusicPlayer music;
        public SoundPlayer soundPlayer;

        public Ludum46()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            UnscaledWidth = 256;
            UnscaledHeight = 200;
            Scale = 4;

            graphics.PreferredBackBufferWidth = (int)(UnscaledWidth * Scale);
            graphics.PreferredBackBufferHeight = (int)(UnscaledHeight * Scale);
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            this.Window.Title = "Ludum46";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pool = new ObjectPool(this);
            level = new Level.Level(this);

            screenPool = new ScreenPool(this);

            music = new MusicPlayer(this);
            soundPlayer = new SoundPlayer(this);

            AttackEffectPool.LoadAssets(this);
            SharedElements.LoadAssets(this);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaUpdate = gameTime.ElapsedGameTime.Milliseconds;

            this.screenPool.CallGuiControlUpdates(this);

            if (this.screenPool.screenState == ScreenPool.ScreenState.Playing)
            {
                PlayerController.UpdateControls(this);
            }

            this.music.Update(this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DeltaDraw = gameTime.ElapsedGameTime.Milliseconds;

            this.screenPool.CallDraws(this, spriteBatch, this.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
