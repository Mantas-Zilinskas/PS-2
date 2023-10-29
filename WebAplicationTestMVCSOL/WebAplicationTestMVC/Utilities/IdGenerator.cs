using System;

namespace WebAplicationTestMVC.Utilities
{
    public static class IdGenerator
    {
        public static string GenerateId<T>(T seed1, T seed2, int maxRandom = 10000000)
            where T : class, IComparable<T> // This line enforces that T must implement IComparable<T>
        {
            Random random = new Random();

            if (seed1 is null || seed2 is null)
            {
                throw new ArgumentNullException(nameof(seed1), "The seed arguments cannot be null.");
            }

            // Utilizing IComparable<T> to compare seed1 and seed2
            int comparisonResult = seed1.CompareTo(seed2);

            string comparisonIndicator;
            if (comparisonResult < 0)
            {
                comparisonIndicator = "LT"; 
            }
            else if (comparisonResult > 0)
            {
                comparisonIndicator = "GT";
            }
            else
            {
                comparisonIndicator = "EQ"; 
            }

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string randomValue = random.Next(1, maxRandom).ToString();
            string id = $"{dateTime} {seed1}_{seed2}_{comparisonIndicator}_{randomValue}";

            return id;
        }
    }
}
