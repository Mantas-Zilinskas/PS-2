namespace WebAplicationTestMVC.Utilities
{
    public static class IdGenerator 
    {
        public static string GenerateId<T>(T seed1, T seed2, int maxRandom = 10000000)
        {
            Random random = new Random();

            string id = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            id = $"{id} {seed1}_{seed2}_{random.Next(1, maxRandom)}";

            return id;
        }
    }
}
