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

        public void SetBoundingBox()
        {
            _boundingBox = new Rectangle((int)_position.X, (int)_position.Y, _spriteTexture.Width, _spriteTexture.Height);
        }

        public Rectangle GetBoundingBox()
        {
            return _boundingBox;
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
