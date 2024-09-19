using System.Collections;
using System.Collections.Generic;

namespace FishPlanner
{
    public class FishingPole
    {
        private int price;
        private Size size;

        public int Price => price;
        public Size Size => size;

        public FishingPole(int price, Size size)
        {
            this.price = price;
            this.size = size;
        }
    }
}
