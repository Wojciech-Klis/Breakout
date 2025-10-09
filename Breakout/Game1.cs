using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _gameOverText;
        private Sprite _paddle, _ball, _jerzy;
        private Sprite[,] _bricks = new Sprite[5,11];
        private bool _ballRight, _ballLeft, _ballUp, _ballDown = true, _gameOver = false;
        private int _lives = 3;
        string _winText = $"Game Over!";
        private Texture2D _paddleTexture, _ballTexture, _brickTexture, _gameOverScreen;
        private Vector2 _paddlePos, _ballPos, _gameOverTextPos;
        private Rectangle _ballBox, _paddleBox;
        Random _random = new Random();

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
            _gameOverText = Content.Load<SpriteFont>("gameOver");
            _gameOverScreen = Content.Load<Texture2D>("jerzy tomaszewski");

            _paddlePos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - _paddleTexture.Width / 2,
                _graphics.PreferredBackBufferHeight - _paddleTexture.Height);
            _paddle = new Sprite(_paddleTexture, _paddlePos,
                12.0f, Color.White);

            _ballPos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - _ballTexture.Width / 2,
                _graphics.PreferredBackBufferHeight / 2 - _ballTexture.Height / 2);
            _ball = new Sprite(_ballTexture, _ballPos,
                8.0f, Color.White);

            Vector2 textSizeWin = _gameOverText.MeasureString(_winText);
            _gameOverTextPos = new Vector2((_graphics.PreferredBackBufferWidth / 2f) - (textSizeWin.X / 2f),
                (_graphics.PreferredBackBufferHeight / 2f) - (textSizeWin.Y / 2f));


            int _gap = 10;
            int _brickWidth = _brickTexture.Width;
            int _brickHeight = _brickTexture.Height;
            int _screenWidth = _graphics.PreferredBackBufferWidth;
            int _screenHeight = _graphics.PreferredBackBufferHeight;
            int _cols = 11;
            int _rows = 6;
            int _totalWidth = _cols * _brickWidth + (_cols - 1) * _gap;
            int _startX = (_screenWidth - _totalWidth) / 2;
            int _startY = 10; 

            _bricks = new Sprite[_rows, _cols];

            // create the brick grid
            for (int _row = 0; _row < _rows; _row++)
            {
                for (int _col = 0; _col < _cols; _col++)
                {
                    float x = _startX + _col * (_brickWidth + _gap);
                    float y = _startY + _row * (_brickHeight + _gap);

                    _bricks[_row, _col] = new Sprite(_brickTexture, new Vector2(x, y), 0f, Color.White);
                }
            }
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
                _lives -= 1;
                _ball.GetPosition();
                _ballPos.X = (float)(_graphics.PreferredBackBufferWidth / 2) - (_ballTexture.Width / 2);
                _ballPos.Y = (float)(_graphics.PreferredBackBufferHeight / 2) - (_ballTexture.Height / 2);
                _ballLeft = false;
                _ballRight = false;
                _ball.SetPosition(_ballPos);

                if (_lives < 1)
                {
                    _gameOver = true;
                }
            }

            if (_ballBox.Intersects(_paddleBox))
            {
                _ballUp = true;
                _ballDown = false;
            }

            if (_ballDown == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.Y += _paddle.GetVelocity();
                _ball.SetPosition(_ballPos);

            }

            if (_ballUp == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.Y -= _paddle.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            if (_ballLeft == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.X -= _ball.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            if (_ballRight == true)
            {
                _ballPos = _ball.GetPosition();
                _ballPos.X += _ball.GetVelocity();
                _ball.SetPosition(_ballPos);
            }

            for (int _row = 0; _row < _bricks.GetLength(0); _row++)
            {
                for (int _col = 0; _col < _bricks.GetLength(1); _col++)
                {
                    Sprite _brick = _bricks[_row, _col];
                    if (_brick == null) continue;

                    _brick.SetBoundingBox();

                    Rectangle _brickBox = _brick.GetBoundingBox();

                    if (_ballBox.Intersects(_brickBox))
                    {
                        _bricks[_row, _col] = null;
                        _ball.GetPosition();
                        _ballUp = false;
                        _ballDown = true;
                        _ball.SetPosition(_ballPos);
                        _row = _bricks.GetLength(0);
                        break;
                    }
                }
            }

            if (_ballBox.Intersects(_paddleBox) && _ballLeft == false && _ballRight == false)
            {
                int _randomDirection = _random.Next(1, 3);

                if (_randomDirection == 1)
                {
                    _ballLeft = true;
                }
                if (_randomDirection == 2)
                {
                    _ballRight = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.IndianRed);

            // TODO: Add your drawing code here
            
            _spriteBatch.Begin();

            if (_gameOver == false)
            {
                _spriteBatch.Draw(_gameOverScreen, new Vector2(0, 0), Color.White);
                _paddle.DrawSprite(_spriteBatch);
                _ball.DrawSprite(_spriteBatch);
                for (int _row = 0; _row < _bricks.GetLength(0); _row++)
                {
                    for (int _col = 0; _col < _bricks.GetLength(1); _col++)
                    {
                        _bricks[_row, _col]?.DrawSprite(_spriteBatch);
                    }
                }
            }

            if (_gameOver == true)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.DrawString(_gameOverText, _winText, _gameOverTextPos, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
