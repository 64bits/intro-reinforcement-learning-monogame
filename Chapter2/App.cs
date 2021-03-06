﻿using System;
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

        private TestBed _tB;
        private SampleAvgAgent _sampleAgent;
        private StepSizeAgent _stepAgent;
        private Graph _graph;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<float> _sampleAvgRewards;
        private List<float> _stepSizeRewards;

        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            // Break out of the FPS
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            _tB = new TestBed(STARTING_REWARD);
            _sampleAgent = new SampleAvgAgent(_tB);
            _stepAgent = new StepSizeAgent(_tB);
            _graph = new Graph(graphics.GraphicsDevice, new Point(800, 600))
            {
                Position = new Vector2(0, 600),
                MaxValue = 20
            };

            _sampleAvgRewards = new List<float>();
            _stepSizeRewards = new List<float>();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_currentSteps < MAX_STEPS)
            {
                _sampleAgent.SampleBandit();
                _stepAgent.SampleBandit();
                _currentSteps++;
                _tB.Step();
                _sampleAvgRewards.Add((float)(_sampleAgent.TotalReward/_currentSteps));
                _stepSizeRewards.Add((float)(_stepAgent.TotalReward/_currentSteps));
            }
            else
            {
                Plot plot = new Plot();
                plot.AddSeries(_sampleAvgRewards, "Sample Average", 211);
                plot.AddSeries(_stepSizeRewards, "Step Size", 360);
                plot.Export();
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            _graph.Draw(_sampleAvgRewards, Color.Red);
            _graph.Draw(_stepSizeRewards, Color.Blue);
            base.Draw(gameTime);
        }
    }
}
