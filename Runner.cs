using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DevilEscape
{
    class Runner
    {
        public Vector2 position = new Vector2(300, 300);
        public static Vector2 dPosition = new Vector2(300, 300);
        public int boundary = 30;
        public int speed = 400;

        public void runnerUpdate(GameTime mGTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float timeUp = (float) mGTime.ElapsedGameTime.TotalSeconds;

            position.X += speed * timeUp * (keyboardState.IsKeyDown(Keys.Right) ? 1 : 0);
            position.X -= speed * timeUp * (keyboardState.IsKeyDown(Keys.Left) ? 1 : 0);
            position.Y -= speed * timeUp * (keyboardState.IsKeyDown(Keys.Up) ? 1 : 0);
            position.Y += speed * timeUp * (keyboardState.IsKeyDown(Keys.Down) ? 1 : 0);
        }
    }
}
 