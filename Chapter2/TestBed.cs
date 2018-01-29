using System;

namespace Chapter2
{
    /// <summary>
    /// This is the modified 10-armed bandit test bed, wherein all machines start of with the same average
    /// reward, but slowly perform a random walk
    /// </summary>
    public class TestBed
    {
        private const int NUMBER_OF_BANDITS = 10;
        private double[] _rewards;
        private Random _random;

        public TestBed(double initialReward)
        {
            _random = new Random(NUMBER_OF_BANDITS);
            _rewards = new double[NUMBER_OF_BANDITS];
            for (int i = 0; i < NUMBER_OF_BANDITS; i++)
            {
                _rewards[i] = initialReward;
            }
        }

        public double Sample(int banditIndex)
        {
            double variance = (_random.NextDouble() - 0.5) * 2; // Allow a variance of 1 in either direction
            return _rewards[banditIndex] + variance;
        }

        public void Step()
        {
            double variance;
            for (int i = 0; i < NUMBER_OF_BANDITS; i++)
            {
                variance = (_random.NextDouble() - 0.5) * 0.02; // Allow a variance of 0.01 in either direction
                _rewards[i] += variance;
            }
        }
    }
}