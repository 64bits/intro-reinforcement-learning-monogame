using System;

namespace Chapter2
{
    /// <summary>
    /// This agent uses sample averages to pick its next action
    /// </summary>
    public class StepSizeAgent : Agent
    {
        private const double STEP_SIZE = 0.1;
        
        public StepSizeAgent(TestBed testBed) : base(testBed) {}

        public override double SampleBandit()
        {
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

            double sampledValue = _testBed.Sample(banditIndex);
            _numSelections[banditIndex]++;
            _averages[banditIndex] = _averages[banditIndex] +
                                     STEP_SIZE * (sampledValue - _averages[banditIndex]);
            TotalReward += sampledValue;
            return sampledValue;
        }
    }
}