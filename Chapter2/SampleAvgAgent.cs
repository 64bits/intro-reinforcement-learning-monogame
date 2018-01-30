using System;

namespace Chapter2
{
    /// <summary>
    /// This agent uses sample averages to pick its next action
    /// </summary>
    public class SampleAvgAgent
    {
        private const int NUMBER_OF_BANDITS = 10;
        private const double EPSILON = 0.1;
        private readonly TestBed _testBed;
        private readonly double[] _averages;
        private readonly int[] _numSelections;
        private readonly Random _random;

        public double TotalReward;
        
        public SampleAvgAgent(TestBed testBed)
        {
            _testBed = testBed;
            _averages = new double[NUMBER_OF_BANDITS];
            _numSelections = new int[NUMBER_OF_BANDITS];
            _random = new Random(NUMBER_OF_BANDITS);
        }

        public double SampleBandit()
        {
            double sampledValue;
            int banditIndex = 0;
            if (IsEpsilonCase())
            {
                banditIndex = _random.Next(NUMBER_OF_BANDITS);
            }
            else
            {
                for (int i = 0; i < NUMBER_OF_BANDITS; i++)
                {
                    if (_averages[i] > _averages[banditIndex]) banditIndex = i;
                }
            }

            sampledValue = _testBed.Sample(banditIndex);
            _numSelections[banditIndex]++;
            _averages[banditIndex] = _averages[banditIndex] +
                                     (1 / _numSelections[banditIndex]) * (sampledValue - _averages[banditIndex]);
            TotalReward += sampledValue;
            return sampledValue;
        }

        private bool IsEpsilonCase()
        {
            return _random.NextDouble() < EPSILON;
        }
    }
}