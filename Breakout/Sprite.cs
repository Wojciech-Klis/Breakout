using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    internal class Sprite
    {
        private Texture2D _spriteTexture;
        private Vector2 _position;
        private float _velocity;
        private Rectangle _boundingBox;
        private Color _color;

        public Sprite()
        {
        }

        public Sprite(Texture2D texture, Vector2 position, float velocity, Color color)
        {
            _spriteTexture = texture;
            _position = position;
            _velocity = velocity;
            _color = color;

        }

        public void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteTexture, _position, _color);
        }

        public void Update()
        {
            _boundingBox = new Rectangle();
            _boundingBox.X = (int)_position.X; 
            _boundingBox.Y = (int)_position.Y;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public float GetVelocity()
        {
            return _velocity;
        }

        public void SetVelocity(float velocity)
        {
            _velocity = velocity;
        }
    }
}
