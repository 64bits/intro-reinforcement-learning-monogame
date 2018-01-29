using System;
using System.Collections.Generic;
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
        private Graph _graph;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<float> _sampleAvgRewards;

        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _tB = new TestBed(STARTING_REWARD);
            _sA = new SampleAvgAgent(_tB);
            _graph = new Graph(graphics.GraphicsDevice, new Point(800, 600))
            {
                Position = new Vector2(0, 300),
                MaxValue = 5
            };

            _sampleAvgRewards = new List<float>();
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
                _sampleAvgRewards.Add((float)(_totalReward/_currentSteps));
                _currentSteps++;
            }
            else
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            _graph.Draw(_sampleAvgRewards, Color.Red);
            base.Draw(gameTime);
        }
    }
}
