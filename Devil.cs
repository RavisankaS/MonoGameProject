using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;

namespace DevilEscape
{
    class Devil
    {
        public Vector2 position;
        public int speed;
        public int boundary;
        public bool passed;
        private float yPosition;

        public Devil(int devSpeed)
        {
            speed = devSpeed;
            Random random = new Random();
            position = new Vector2(1000, random.Next(0, 700));
            boundary = 60;
            yPosition = 0;
        }

        public void devilUpdate(GameTime gameTime)
        {
            float timeUp = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float motionVal = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 5);
            yPosition = motionVal * 10;

            position.X -= speed * timeUp;
            position.Y += yPosition;
        }
    }
}

