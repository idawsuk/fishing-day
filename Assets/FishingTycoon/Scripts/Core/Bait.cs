using System.Collections;
using System.Collections.Generic;

namespace FishPlanner
{
    public class Bait
    {
        private int price;
        private Color color;

        public int Price => price;
        public Color Color => color;

        public Bait(int price, Color color)
        {
            this.price = price;
            this.color = color;
        }
    }
}