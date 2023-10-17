namespace WebAplicationTestMVC.Utilities
{
    public class IdGenerator<T>
    {
        public static string GenerateId(T seed1, T seed2)
        {
            Random random = new Random();

            string id = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            id = id + " " + seed1.ToString() + "_" + seed2.ToString() + "_" + random.Next(1, 10000000);

            return id;
        }
    }
}
