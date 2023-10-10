namespace WebAplicationTestMVC.Utilities
{
    public class IdGenerator
    {
        public static string generateId(string seed1, string seed2)
        {
            Random random = new Random();

            string id = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            id = id + " " + seed1.Length + "_" + seed2.Length + "_" + random.Next(1, 10000000);

            return id;
        }
    }
}
