using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chapter2
{
    public class App : Game
    {
        private const int MAX_STEPS = 10000;
        private const double STARTING_REWARD = 10;
        private int _currentSteps;
        private double _totalReward;

        private TestBed _tB;
        private SampleAvgAgent _sA;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _tB = new TestBed(STARTING_REWARD);
            _sA = new SampleAvgAgent(_tB);
            base.Initialize();
        }
//
//        protected override void LoadContent()
//        {
//            spriteBatch = new SpriteBatch(GraphicsDevice);
//
//            // TODO: use this.Content to load your game content here
//        }
//
        protected override void Update(GameTime gameTime)
        {
            if (_currentSteps < MAX_STEPS)
            {
                _totalReward += _sA.SampleBandit();
                Console.WriteLine(_currentSteps + "," + _totalReward/_currentSteps);
                _currentSteps++;
            }
            else
            {
                Exit();
            }

            base.Update(gameTime);
        }
//
//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.CornflowerBlue);
//
//            // TODO: Add your drawing code here
//
//            base.Draw(gameTime);
//        }
    }
}
