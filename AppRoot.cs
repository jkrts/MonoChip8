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

            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.D1))ch8.keys[0x1] = 1;
            if(keyboardState.IsKeyDown(Keys.D2))ch8.keys[0x2] = 1; 
            if(keyboardState.IsKeyDown(Keys.D3))ch8.keys[0x3] = 1; 
            if(keyboardState.IsKeyDown(Keys.D4))ch8.keys[0xC] = 1; 
            if(keyboardState.IsKeyDown(Keys.Q))ch8.keys[0x4] = 1; 
            if(keyboardState.IsKeyDown(Keys.W))ch8.keys[0x5] = 1; 
            if(keyboardState.IsKeyDown(Keys.E))ch8.keys[0x6] = 1; 
            if(keyboardState.IsKeyDown(Keys.R))ch8.keys[0xD] = 1; 
            if(keyboardState.IsKeyDown(Keys.A))ch8.keys[0x7] = 1; 
            if(keyboardState.IsKeyDown(Keys.S))ch8.keys[0x8] = 1; 
            if(keyboardState.IsKeyDown(Keys.D))ch8.keys[0x9] = 1; 
            if(keyboardState.IsKeyDown(Keys.F))ch8.keys[0xE] = 1; 
            if(keyboardState.IsKeyDown(Keys.Z))ch8.keys[0xA] = 1; 
            if(keyboardState.IsKeyDown(Keys.X))ch8.keys[0x0] = 1; 
            if(keyboardState.IsKeyDown(Keys.C))ch8.keys[0xB] = 1;
            if(keyboardState.IsKeyDown(Keys.V))ch8.keys[0xF] = 1;  

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
