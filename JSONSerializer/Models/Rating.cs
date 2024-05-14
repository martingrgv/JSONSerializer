namespace JSONSerializer.Models
{
    public class Rating
    {
        private int[] rate;

        public Rating()
        {
            rate = new int[5];
        }

        public Rating(int size)
        {
            rate = new int[size];
        }

        public int Rate { get => GetRating(); }

        public void AddRating()
        {
            for (int i = 0; i < rate.Length; i++)
            {
                if (rate[i] == 0)
                {
                    rate[i] = 1;
                }
            }
        }

        private int GetRating()
        {
            int stars = 0;

            foreach (var r in rate)
            {
                if (r == 1)
                {
                    stars++;
                }
            }

            return stars;
        }
    }
}
