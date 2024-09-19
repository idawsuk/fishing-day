using System;
using System.Collections;
using System.Collections.Generic;

namespace FishPlanner
{
    public class Fish
    {
        private int minReward;
        private int maxReward;
        private Size size;
        private Color color;
        private int reward;

        private Random random;

        public int Reward
        {
            get
            {
                return reward;
            }
        }
        public int MinReward => minReward;
        public int MaxReward => maxReward;

        public Size Size => size;
        public Color Color => color;

        public Fish(int minReward, int maxReward, Size size, Color color)
        {
            this.minReward = minReward;
            this.maxReward = maxReward;
            this.size = size;
            this.color = color;

            random = new Random();//put seed here if necessary

            reward = random.Next(minReward, maxReward + 1);
        }

        public Fish(Size size, Color color)
        {
            this.size = size;
            this.color = color;
        }
    }
}
