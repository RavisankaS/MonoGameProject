using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DevilEscape
{
    class DevilAction
    {
        public List<Devil> devils = new List<Devil>();
        public bool runGame = false;
        public double fullTime = 0;
        public double cdTimer = 1;
        public int speedUp = 240;
        public double highestTime = 1;
        
        public void dvActionUpdate(GameTime mGTime)
        {
            if (runGame)
            {
                cdTimer -= mGTime.ElapsedGameTime.TotalSeconds;
                fullTime += mGTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    runGame = true;
                    fullTime = 0;
                    cdTimer = 1;
                    speedUp = 225;
                    highestTime = 1.8;
                }
            }

            if (cdTimer <= 0)
            {
                devils.Add(new Devil(speedUp));
                cdTimer = highestTime;

                if (speedUp < 620)
                {
                    speedUp += 3;
                }

                if (highestTime > 0.6)
                {
                    highestTime -= 0.1;
                }

            }
        }
    }
}
