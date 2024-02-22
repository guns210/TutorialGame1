using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TutorialGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite;
        Texture2D crosshairsSprite;
        Texture2D backgroundSprite;
        SpriteFont gameFont;

        Vector2 targetPossition = new Vector2(300,300);
        const int targetRadius = 45;

        MouseState mouseState;
        bool mouseReleased = true;
        int score = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed && mouseReleased == true)
            {
                float mouseTargetDistance = Vector2.Distance(targetPossition, mouseState.Position.ToVector2());
                if(mouseTargetDistance < targetRadius)
                {
                    score++;

                    Random random = new Random();
                    targetPossition.X = random.Next(0, _graphics.PreferredBackBufferWidth);
                    targetPossition.Y = random.Next(0, _graphics.PreferredBackBufferHeight);
                }
                mouseReleased = false;
            }

            if(mouseState.LeftButton == ButtonState.Released)
            {
                mouseReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(gameFont, score.ToString(), new Vector2(100, 100), Color.White);
            _spriteBatch.Draw(targetSprite, new Vector2(targetPossition.X - targetRadius , targetPossition.Y - targetRadius), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
