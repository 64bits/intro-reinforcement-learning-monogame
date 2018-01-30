using System;

namespace Chapter2
{
    /// <summary>
    /// Generic agent class to be extended by implementers
    /// </summary>
    public abstract class Agent
    {
        protected const int NUMBER_OF_BANDITS = 10;
        private const double EPSILON = 0.1;
        protected readonly TestBed _testBed;
        protected readonly double[] _averages;
        protected readonly int[] _numSelections;
        protected readonly Random _random;

        public double TotalReward;

        protected Agent(TestBed testBed)
        {
            _testBed = testBed;
            _averages = new double[NUMBER_OF_BANDITS];
            _numSelections = new int[NUMBER_OF_BANDITS];
            _random = new Random(NUMBER_OF_BANDITS);
        }

        public abstract double SampleBandit();

        protected bool IsEpsilonCase()
        {
            return _random.NextDouble() < EPSILON;
        }
    }
}