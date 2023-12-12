using WebAplicationTestMVC.Utilities;

namespace WebApplicationTestMVCTests
{
    [TestClass]
    public class IdGeneratorTests
    {
        [TestMethod]
        public void GenerateId_CompareResult_LT()
        {
            var seed1 = "Apple";
            var seed2 = "Banana";

            var id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "LT");
        }

        [TestMethod]
        public void GenerateId_CompareResult_GT()
        {
            var seed1 = "Banana";
            var seed2 = "Apple";

            // Act
            var id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "GT");
        }

        [TestMethod]
        public void GenerateId_CompareResult_EQ()
        {
            var seed1 = "Apple";
            var seed2 = "Apple";

            // Act
            var id = IdGenerator.GenerateId(seed1, seed2);

            Assert.IsNotNull(id);
            StringAssert.Contains(id, "EQ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateId_NullSeed1_ThrowsException()
        {
            string seed1 = null;
            var seed2 = "Banana";

            IdGenerator.GenerateId(seed1, seed2);

            // Expecting ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateId_NullSeed2_ThrowsException()
        {
            var seed1 = "Apple";
            string seed2 = null;

            // Act
            var id = IdGenerator.GenerateId(seed1, seed2);

            // Expecting ArgumentNullException
        }
    }
}