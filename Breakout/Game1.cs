using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprite _paddle;
        private Texture2D _paddleTexture;
        private Vector2 _paddlePos;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
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

            _paddleTexture = Content.Load<Texture2D>("paddle");

            _paddlePos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - _paddleTexture.Width / 2, _graphics.PreferredBackBufferHeight - _paddleTexture.Height);
            _paddle = new Sprite(_paddleTexture, _paddlePos,
                8.0f, Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (_paddlePos > 0.0f)
                {
                    Vector2 pos = _paddle.GetPosition();
                    pos.X -= _paddle.GetVelocity();
                    _paddle.SetPosition(pos);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (_paddlePos.X < _graphics.PreferredBackBufferWidth - _paddleTexture.Width)
                {
                    Vector2 pos = _paddle.GetPosition();
                    pos.X += _paddle.GetVelocity();
                    _paddle.SetPosition(pos);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _paddle.DrawSprite(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
