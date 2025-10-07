using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprite _paddle, _ball, _brick;
        private Sprite[,] _bricks = new Sprite[5, 5];
        private bool _ballRight, _ballLeft = true, _ballUp, _ballDown = true;
        private Texture2D _paddleTexture, _ballTexture, _brickTexture;
        private Vector2 _paddlePos, _ballPos;
        private float _ballVelocity;
        private Rectangle _ballBox, _paddleBox;

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
            _ballTexture = Content.Load<Texture2D>("ball");
            _brickTexture = Content.Load<Texture2D>("brick");

            _paddlePos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - _paddleTexture.Width / 2,
                _graphics.PreferredBackBufferHeight - _paddleTexture.Height);
            _paddle = new Sprite(_paddleTexture, _paddlePos,
                8.0f, Color.White);

            _ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - _ballTexture.Width / 2,
                _graphics.PreferredBackBufferHeight / 2 - _ballTexture.Height / 2);
            _ball = new Sprite(_ballTexture, _ballPos,
                6.0f, Color.White);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _ball.SetBoundingBox();
            _paddle.SetBoundingBox();
            _ballBox = _ball.GetBoundingBox();
            _paddleBox = _paddle.GetBoundingBox();


            if (Keyboard.GetState().IsKeyDown(Keys.A) && _paddlePos.X >= 0)
            {
                _paddlePos = _paddle.GetPosition();
                _paddlePos.X -= _paddle.GetVelocity();
                _paddle.SetPosition(_paddlePos);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)
                && _paddlePos.X + _paddleTexture.Width < _graphics.PreferredBackBufferWidth)
            {
                _paddlePos = _paddle.GetPosition();
                _paddlePos.X += _paddle.GetVelocity();
                _paddle.SetPosition(_paddlePos);
            }

            if (_ballPos.X <= 0)
            {
                _ballRight = true;
                _ballLeft = false;
            }

            if (_ballPos.X >= _graphics.PreferredBackBufferWidth - _ballTexture.Width)
            {
                _ballLeft = true;
                _ballRight = false;
            }

            if (_ballPos.Y <= 0)
            {
                _ballDown = true;
                _ballUp = false;
            }

            if (_ballPos.Y >= _graphics.PreferredBackBufferHeight - _ballTexture.Height)
            {
                _ballUp = true;
                _ballDown = false;
            }

            if (_ballBox.Intersects(_paddleBox))
            {
                _ballUp = true;
                _ballDown = false;
            }

            if (_ballDown == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.Y += _ball.GetVelocity();
                _ball.SetPosition(_ballPos);

            }

            if (_ballUp == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.Y -= _ball.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            if (_ballLeft == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.X -= _paddle.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            if (_ballRight == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.X += _paddle.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();
            _paddle.DrawSprite(_spriteBatch);
            _ball.DrawSprite(_spriteBatch);
            foreach (_brick in _bricks)
            {
                _brick.DrawSprite(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
