using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class IdGeneratorTests
    {
        [TestMethod]
        public void GenerateId_CompareResult_LT()
        {
            string seed1 = "Apple";
            string seed2 = "Banana";

            string id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "LT");
        }

        [TestMethod]
        public void GenerateId_CompareResult_GT()
        {
            string seed1 = "Banana";
            string seed2 = "Apple";

            // Act
            string id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "GT");
        }

        [TestMethod]
        public void GenerateId_CompareResult_EQ()
        {
            string seed1 = "Apple";
            string seed2 = "Apple";

            // Act
            string id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "EQ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateId_NullSeed1_ThrowsException()
        {
            string seed1 = null;
            string seed2 = "Banana";

            string id = IdGenerator.GenerateId(seed1, seed2);

            // Expecting ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateId_NullSeed2_ThrowsException()
        {
            string seed1 = "Apple";
            string seed2 = null;

            // Act
            string id = IdGenerator.GenerateId(seed1, seed2);

            // Expecting ArgumentNullException
        }
    }
}
