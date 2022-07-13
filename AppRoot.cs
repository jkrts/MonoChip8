using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

            ch8.LoadRom("-Untitled-");
            //ch8.LoadRom("Roms/test_opcode.ch8");
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

            if(keyboardState.IsKeyDown(Keys.D1))
            {
                ch8.keys[0x1] = 1;
                Console.WriteLine("1");
            }
            if(keyboardState.IsKeyDown(Keys.D2))
            {
                ch8.keys[0x2] = 1;
                Console.WriteLine("2");
            }
            if(keyboardState.IsKeyUp(Keys.D1))
            {
                ch8.keys[0x1] = 0;
            }
            if(keyboardState.IsKeyUp(Keys.D2))
            {
                ch8.keys[0x2] = 0;
            }

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
