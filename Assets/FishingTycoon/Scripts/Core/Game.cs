using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FishPlanner
{
    public class Game
    {
        private FishingPole[] fishingPoles;
        private Bait[] baits;
        private Fish[] fish;

        private Player player;
        private int day;
        private FishForecast forecast;
        private List<Fish> fishCatched;

        public FishingPole[] FishingPoles => fishingPoles;
        public Bait[] Baits => baits;
        public FishForecast Forecast => forecast;
        public Player Player => player;
        public int Day => day;

        public Game()
        {
            //initiate data
            fishingPoles = new FishingPole[3];
            fishingPoles[0] = new FishingPole(5, Size.Small);
            fishingPoles[1] = new FishingPole(10, Size.Medium);
            fishingPoles[2] = new FishingPole(15, Size.Big);

            baits = new Bait[3];
            baits[0] = new Bait(1, Color.Red);
            baits[1] = new Bait(2, Color.Blue);
            baits[2] = new Bait(3, Color.Green);

            fish = new Fish[9];
            fish[0] = new Fish(1, 5, Size.Small, Color.Red);
            fish[1] = new Fish(5, 10, Size.Medium, Color.Red);
            fish[2] = new Fish(10, 15, Size.Big, Color.Red);
            fish[3] = new Fish(3, 5, Size.Small, Color.Blue);
            fish[4] = new Fish(8, 10, Size.Medium, Color.Blue);
            fish[5] = new Fish(13, 15, Size.Big, Color.Blue);
            fish[6] = new Fish(5, 5, Size.Small, Color.Green);
            fish[7] = new Fish(10, 10, Size.Medium, Color.Green);
            fish[8] = new Fish(15, 15, Size.Big, Color.Green);

            //starting items
            player = new Player();

            forecast = new FishForecast(2, 21, fish);
        }

        public void StartDay()
        {
            day++;
            forecast.GenerateForecast();
            player.ClearBait();
        }

        public FishingResults StartFishing()
        {
            fishCatched = new List<Fish>();
            FishingPole fishingPole = player.FishingPole;
            Dictionary<Color, Queue<Fish>> fishColor = new Dictionary<Color, Queue<Fish>>();
            fishColor.Add(Color.Red, new Queue<Fish>());
            fishColor.Add(Color.Blue, new Queue<Fish>());
            fishColor.Add(Color.Green, new Queue<Fish>());
            for (int i = 0; i < forecast.Fish.Count; i++)
            {
                Fish fish = forecast.Fish[i];
                if(fish.Size == fishingPole.Size)
                {
                    fishColor[fish.Color].Enqueue(fish);
                }
            }

            int goldEarned = 0;
            foreach(var baitDictionary in player.Baits)
            {
                List<Bait> baits = baitDictionary.Value;
                for (int i = 0; i < baits.Count; i++)
                {
                    Bait bait = baits[i];
                    if(fishColor.TryGetValue(bait.Color, out var fishes)) {
                        if(fishes.Count > 0)
                        {
                            Fish fish = fishes.Dequeue();
                            goldEarned += fish.Reward;
                            fishCatched.Add(fish);
                        }
                    }
                }
            }

            player.EarnGold(goldEarned);
            player.FishingPole = null;

            FishingResults result = new FishingResults();
            result.Fish = fishCatched;
            result.GoldEarned = goldEarned;
            result.TotalGold = player.Gold;
            result.Survived = player.Gold > 100;

            return result;
        }

        public bool BuyFishingPole(Size size)
        {
            FishingPole pole = GetFishingPole(size);
            if (pole != null)
            {
                if (player.UseGold(pole.Price))
                {
                    player.FishingPole = pole;
                    return true;
                }
            }

            return false;
        }

        public bool BuyBait(List<Bait> baitToBuy)
        {
            int totalPrice = 0;
            for (int i = 0; i < baitToBuy.Count; i++)
            {
                Bait bait = GetBait(baitToBuy[i].Color);
                totalPrice += bait.Price;
            }

            if(player.UseGold(totalPrice))
            {
                player.AddBait(baitToBuy);
                return true;
            }

            return false;
        }

        public bool BuyBait(Color color)
        {
            Bait bait = GetBait(color);
            if(bait != null)
            {
                if(player.UseGold(bait.Price))
                {
                    player.AddBait(bait);
                    return true;
                }
            }

            return false;
        }

        private FishingPole GetFishingPole(Size size)
        {
            for (int i = 0; i < fishingPoles.Length; i++)
            {
                if (fishingPoles[i].Size == size)
                {
                    return fishingPoles[i];
                }
            }

            return null;
        }

        public Bait GetBait(Color color)
        {
            for (int i = 0; i < baits.Length; i++)
            {
                if (baits[i].Color == color)
                {
                    return baits[i];
                }
            }

            return null;
        }

        public class FishingResults
        {
            public List<Fish> Fish;
            public int GoldEarned;
            public int TotalGold;
            public bool Survived;
        }

        public class FishForecast
        {
            private Dictionary<Size, int> fishSize;
            private Dictionary<int, Color> fishColor;
            private int minFishCount;
            private int maxFishCount;
            private List<Fish> fish;
            private Fish[] fishData;

            private Random random;

            public Dictionary<Size, int> FishSize => fishSize;
            public Dictionary<Color, float> FishColor
            {
                get
                {
                    Dictionary<Color, float> result = new Dictionary<Color, float>();
                    result.Add(Color.Red, GetFishColorChance(Color.Red));
                    result.Add(Color.Blue, GetFishColorChance(Color.Blue));
                    result.Add(Color.Green, GetFishColorChance(Color.Green));
                    return result;
                }
            }
            public List<Fish> Fish => fish;

            public FishForecast(int minFishCount, int maxFishCount, Fish[] data)
            {
                random = new Random();
                this.maxFishCount = maxFishCount;
                this.minFishCount = minFishCount;
                fishData = data;
            }

            public void GenerateForecast()
            {
                fishSize = new Dictionary<Size, int>();

                fishSize.Add(Size.Small, random.Next(minFishCount, maxFishCount));
                fishSize.Add(Size.Medium, random.Next(minFishCount, maxFishCount));
                fishSize.Add(Size.Big, random.Next(minFishCount, maxFishCount));

                fishColor = new Dictionary<int, Color>();
                for (int i = 0; i < 100; i++)
                {
                    fishColor.Add(i, (Color)random.Next(3));
                }

                fish = new List<Fish>();
                foreach(var size in fishSize)
                {
                    for (int i = 0; i < size.Value; i++)
                    {
                        fish.Add(GetFish(size.Key));
                    }
                }
            }

            private Fish GetFish(Size size)
            {
                if(fishSize.TryGetValue(size, out var fish))
                {
                    if(fish > 0)
                    {
                        Color col = GetColor();
                        for (int i = 0; i < fishData.Length; i++)
                        {
                            if (fishData[i].Size == size && fishData[i].Color == col)
                            {
                                return new Fish(fishData[i].MinReward, fishData[i].MaxReward, size, col);
                            }
                        }
                    }
                }

                return null;
            }

            private float GetFishColorChance(Color color)
            {
                return fishColor.Where(x => x.Value == color).Count() / 100f;
            }

            private Color GetColor()
            {
                return fishColor[random.Next(100)];
            }
        }
    }

    public class Player
    {
        private int gold;
        private FishingPole fishingPole;
        private Dictionary<Color, List<Bait>> baits;

        public delegate void GoldEvent(int gold);
        public GoldEvent OnGoldEventChanged;
        public delegate void FishingPoleEvent(FishingPole pole);
        public FishingPoleEvent OnPoleEventChanged;

        public int Gold
        {
            get { return gold; }
            set
            {
                gold = value;
                OnGoldEventChanged?.Invoke(gold);
            }
        }

        public FishingPole FishingPole
        {
            get { return fishingPole; }
            set { 
                fishingPole = value;
                OnPoleEventChanged?.Invoke(fishingPole);
            }
        }

        public Dictionary<Color, List<Bait>> Baits => baits;

        public Player()
        {
            Gold = 100;

            baits = new Dictionary<Color, List<Bait>>();
            baits.Add(Color.Green, new List<Bait>());
            baits.Add(Color.Red, new List<Bait>());
            baits.Add(Color.Blue, new List<Bait>());
        }

        public bool UseGold(int price)
        {
            if(Gold - price < 0)
            {
                return false;
            }

            Gold -= price;
            return true;
        }

        public void EarnGold(int gold)
        {
            this.Gold += gold;
        }

        public bool UseBait(Color color)
        {
            if(baits.TryGetValue(color, out List<Bait> baitList))
            {
                if(baitList.Count > 0)
                {
                    baitList.RemoveAt(0);
                    return true;
                }
            }

            return false;
        }

        public void AddBait(Bait bait)
        {
            if(baits.TryGetValue(bait.Color, out List<Bait> baitList))
            {
                baitList.Add(bait);
            } else
            {
                List<Bait> newBait = new List<Bait>();
                newBait.Add(bait);

                baits.Add(bait.Color, newBait);
            }
        }

        public void AddBait(List<Bait> baits)
        {
            for (int i = 0; i < baits.Count; i++)
            {
                AddBait(baits[i]);
            }
        }

        public void ClearBait()
        {
            foreach(var bait in baits)
            {
                bait.Value.Clear();
            }
        }
    }
}