using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using System;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DevilEscape
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D runnerMain;
        Texture2D devilMain;
        Texture2D smokeMain;
        SpriteFont gameFont;
        int score = 0;
        int highScore = 0;
        float totalTime = 60f; // Set Total TIme
        float elapsedSeconds = 0f; //Time elapsed in seconds

        Runner gamer = new Runner();
        DevilAction devilAction = new DevilAction();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("dGame");
            devilMain = Content.Load<Texture2D>("devil_SL_2_200x150");
            smokeMain = Content.Load<Texture2D>("smk");
            runnerMain = Content.Load<Texture2D>("runner_100x100");

            // Load high score
            if (File.Exists("highscore.txt"))
            {
                string highScoreText = File.ReadAllText("highscore.txt");
                if (int.TryParse(highScoreText, out int lodHighScore))
                {
                    highScore = lodHighScore;
                }
            }

            /*highScore = 0; // Set high score to 0
            //Save high score of 0 to file
            File.WriteAllText("highscore.txt", highScore.ToString());*/
        }
       
        protected override void Update(GameTime mGTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (devilAction.runGame)
            {
                gamer.runnerUpdate(mGTime);
                elapsedSeconds += (float)mGTime.ElapsedGameTime.TotalSeconds;
                if (elapsedSeconds > totalTime) // End game if time is up
                {
                    devilAction.runGame = false;
                    gamer.position = Runner.dPosition;
                    devilAction.devils.Clear();
                    if (score > highScore) // Update high score
                    {
                        highScore = score;
                        File.WriteAllText("highscore.txt", highScore.ToString()); // Write high score to file
                    }
                    score = 0;
                }
            }

            devilAction.dvActionUpdate(mGTime);

            for (int i = 0; i < devilAction.devils.Count; i++)
            {
                devilAction.devils[i].devilUpdate(mGTime);

                int sum = devilAction.devils[i].boundary  + gamer.boundary ;
                if (Vector2.Distance(devilAction.devils[i].position, gamer.position) < sum)
                {
                    devilAction.runGame = false;
                    gamer.position = Runner.dPosition;
                    devilAction.devils.Clear();
                    if (score > highScore) // Update high score
                    {
                        highScore = score;
                        File.WriteAllText("highscore.txt", highScore.ToString()); // Write high score to file
                    }
                    score = 0;
                }
                else if (devilAction.devils[i].position.X < gamer.position.X && !devilAction.devils[i].passed)
                {
                    devilAction.devils[i].passed = true;
                    score++; // Increase score when runner passes a devil
                }
            }

            if (!devilAction.runGame)
            {
                devilAction.devils.Clear();
                if (score > highScore) // Update high score
                {
                    highScore = score;
                    File.WriteAllText("highscore.txt", highScore.ToString()); // Write high score to file
                }
                score = 0; // Reset score
            }
            base.Update(mGTime);
        }

        protected override void Draw(GameTime mGTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(smokeMain, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(runnerMain, new Vector2(gamer.position.X - 20, gamer.position.Y - 50), Color.White);

            for (int i = 0; i < devilAction.devils.Count; i++)
            {
                _spriteBatch.Draw(devilMain, new Vector2(devilAction.devils[i].position.X - devilAction.devils[i].boundary , devilAction.devils[i].position.Y - devilAction.devils[i].boundary ), Color.White);
            }

            if (devilAction.runGame == false)
            {
                string start = "Press Enter to Begin!";
                Vector2 fSize = gameFont.MeasureString(start);
                int stwidth = _graphics.PreferredBackBufferWidth / 2;

                float stPos = stwidth + (float)Math.Sin(mGTime.TotalGameTime.TotalSeconds * 2.0f) * 50.0f;

                _spriteBatch.DrawString(gameFont, start, new Vector2(stPos - fSize.X / 2, 400), Color.Black);
            }

            _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3, 950), Color.DarkRed);
            _spriteBatch.DrawString(gameFont, "High Score: " + highScore.ToString(), new Vector2(390, 950), Color.DarkRed);
            _spriteBatch.DrawString(gameFont, "Time: " + Math.Floor(devilAction.fullTime).ToString(), new Vector2(270, 10), Color.DarkRed);
            

            _spriteBatch.End();


            base.Draw(mGTime);
        }

    }
}