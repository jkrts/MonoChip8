using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monoChip8
{
    public class AppRoot : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SpriteFont _font;

        Chip8 ch8;
        UIChipScreen uiChipScreen;
        UIChipState uiChipState;
        UIChipMemory uiChipMemory;

        float timer;

        public AppRoot()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            ch8 = new Chip8();
            //ch8.LoadRom("Roms/Keypad_Test.ch8");
            ch8.LoadRom("Roms/test_opcode.ch8");
            //ch8.LoadRom("Roms/font_test.ch8");
            uiChipScreen = new UIChipScreen(_graphics);
            uiChipState = new UIChipState(_graphics);
            uiChipMemory = new UIChipMemory(_graphics);

            timer = 0.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>(@"Fonts\Consolas");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if(timer > 1.0f)
            {
                ch8.EmulateCycle();
                timer = 0.0f;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // TODO: Add your drawing code here
            uiChipScreen.Draw(_spriteBatch, ch8);
            uiChipState.Draw(_spriteBatch, ch8, _font);
            uiChipMemory.Draw(_spriteBatch, ch8, _font);
    
            base.Draw(gameTime);
        }
    }
}
